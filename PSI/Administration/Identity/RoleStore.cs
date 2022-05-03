using Microsoft.AspNetCore.Identity;
using PSI.Data;
using System.Security.Claims;

namespace PSI.Administration.Identity
{
    public class RoleStore : RoleStoreBase<Role, int, UserRole, RoleClaim>
    {
        private readonly IDbSession _session;

        public RoleStore(IDbSession session, IdentityErrorDescriber describer) : base(describer ?? new IdentityErrorDescriber())
        {
            _session = session;
        }

        public override IQueryable<Role> Roles => _session.Query<Role>();

        public override async Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            RoleClaim roleClaim = CreateRoleClaim(role, claim);
            await _session.AddAsync(roleClaim, cancellationToken);
            await _session.SaveChangesAsync();
        }

        public override async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            await _session.AddAsync(role, cancellationToken);
            await _session.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            await _session.RemoveAsync(role, cancellationToken);
            await _session.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public override async Task<Role> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return await _session.FindAsync<Role>(ConvertIdFromString(id), cancellationToken);
        }

        public override Task<Role> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            Role role = Roles.FirstOrDefault(r => r.NormalizedName == normalizedName);

            return Task.FromResult(role);

        }

        public override Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            IList<Claim> claims = _session.Query<RoleClaim>()
                .Where(rc => rc.RoleId == role.Id)
                .Select(c => new Claim(c.ClaimType, c.ClaimValue))
                .ToList();

            return Task.FromResult(claims);
        }

        public override async Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            IEnumerable<RoleClaim> claims = _session.Query<RoleClaim>()
                .Where(
                    rc => rc.RoleId == role.Id
                        && rc.ClaimValue == claim.Value &&
                        rc.ClaimType == claim.Type
                ).ToList();

            foreach (var c in claims)
            {
                await _session.RemoveAsync(c, cancellationToken);
            }

            await _session.SaveChangesAsync(cancellationToken);
        }

        public override async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            bool exists = Roles.Any(r => r.Id == role.Id);
            if (!exists)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "RoleNotExist",
                        Description = $"Role with {role.Id} does not exists."
                    }
                );
            }

            role.ConcurrencyStamp = Guid.NewGuid().ToString("N");

            await _session.AttachAsync(role, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }
    }
}
