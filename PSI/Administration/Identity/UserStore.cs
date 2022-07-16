using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PSI.Data;

namespace PSI.Administration.Identity
{
    public class UserStore : UserStoreBase<User, Role, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        private readonly IDbSession _session;

        public UserStore(IDbSession dbSession, IdentityErrorDescriber describer) : base(describer ?? new IdentityErrorDescriber())
        {
            _session = dbSession;
        }

        public override IQueryable<User> Users => _session.Query<User>();
        private IQueryable<Role> Roles => _session.Query<Role>();
        private IQueryable<UserRole> UserRoles => _session.Query<UserRole>();
        private IQueryable<UserClaim> UserClaims => _session.Query<UserClaim>();
        private IQueryable<UserLogin> UserLogins => _session.Query<UserLogin>();
        private IQueryable<UserToken> UserTokens => _session.Query<UserToken>();

        public override async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }
            foreach (var claim in claims)
            {
                await _session.AddAsync(CreateUserClaim(user, claim), cancellationToken);
            }
            await _session.SaveChangesAsync();
        }

        public override async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            await _session.AddAsync(CreateUserLogin(user, login), cancellationToken);
            await _session.SaveChangesAsync();
        }

        public override async Task AddToRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            Role role = await FindRoleAsync(normalizedRoleName, cancellationToken);

            if (role == null)
            {
                throw new InvalidOperationException(
                    $"Role {normalizedRoleName} not found!"
                );
            }
            await _session.AddAsync(CreateUserRole(user, role), cancellationToken);
            await _session.SaveChangesAsync();
        }

        public override async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _session.AddAsync(user, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _session.RemoveAsync(user, cancellationToken);
            await _session.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            User user = Users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail);

            return Task.FromResult(user);
        }

        public override Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return _session.FindAsync<User>(ConvertIdFromString(userId), cancellationToken);
        }

        public override Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            User user = Users.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName);

            return Task.FromResult(user);
        }

        public override Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            IList<Claim> claims = UserClaims.Where(
                    uc => uc.UserId.Equals(user.Id)
                )
                .Select(c => c.ToClaim())
                .ToList();
            return Task.FromResult(claims);
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            IList<UserLoginInfo> logins = UserLogins.Where(l => l.UserId.Equals(user.Id))
                .Select(
                    l => new UserLoginInfo(
                        l.LoginProvider,
                        l.ProviderKey,
                        l.ProviderDisplayName
                    )
                )
                .ToList();

            return Task.FromResult(logins);
        }

        public override Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            IList<string> roles = (from r in Roles
                                   join ur in UserRoles on r.Id equals ur.RoleId
                                   where ur.UserId == user.Id
                                   select r.Name).ToList();

            return Task.FromResult(roles);
        }

        public override Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            IList<User> users = (from userClaim in UserClaims
                                 join user in Users on userClaim.UserId equals user.Id
                                 where userClaim.ClaimValue == claim.Value
                                     && userClaim.ClaimType == claim.Type
                                 select user).ToList();

            return Task.FromResult(users);
        }

        public override async Task<IList<User>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(normalizedRoleName))
            {
                throw new ArgumentNullException(normalizedRoleName);
            }

            Role role = await FindRoleAsync(
                normalizedRoleName,
                cancellationToken
            );

            if (role != null)
            {
                IList<User> users = (from userRole in UserRoles
                                     join user in Users on userRole.UserId equals user.Id
                                     where userRole.RoleId.Equals(role.Id)
                                     select user).ToList();
                return users;
            }
            return new List<User>();
        }

        public override async Task<bool> IsInRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            Role role = await FindRoleAsync(normalizedRoleName, cancellationToken);

            if (role != null)
            {
                UserRole userRole = await FindUserRoleAsync(
                    user.Id,
                    role.Id,
                    cancellationToken
                );
                return userRole != null;
            }
            return false;
        }

        public override async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            foreach (var claim in claims)
            {
                var matchedClaims = UserClaims.Where(
                        uc => uc.UserId.Equals(user.Id) &&
                            uc.ClaimValue == claim.Value
                            && uc.ClaimType == claim.Type
                    )
                    .ToList();

                foreach (var matchedClaim in matchedClaims)
                {
                    await _session.RemoveAsync(matchedClaim, cancellationToken);
                }
            }
            await _session.SaveChangesAsync(cancellationToken);
        }

        public override async Task RemoveFromRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            Role role = await FindRoleAsync(
                normalizedRoleName,
                cancellationToken
            );

            if (role != null)
            {
                UserRole userRole = await FindUserRoleAsync(
                    user.Id,
                    role.Id,
                    cancellationToken
                );
                if (userRole != null)
                {
                    await _session.RemoveAsync(userRole, cancellationToken);
                    await _session.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public override async Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            UserLogin login = await FindUserLoginAsync(
                user.Id,
                loginProvider,
                providerKey,
                cancellationToken
            );

            if (login != null)
            {
                await _session.RemoveAsync(login, cancellationToken);
                await _session.SaveChangesAsync(cancellationToken);
            }
        }

        public override async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }

            IEnumerable<UserClaim> matchedClaims = UserClaims.Where(
                     uc => uc.UserId.Equals(user.Id) &&
                         uc.ClaimValue == claim.Value
                         && uc.ClaimType == claim.Type
                 )
                 .ToList();

            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimType = newClaim.Type;
                matchedClaim.ClaimValue = newClaim.Value;
                await _session.UpdateAsync(matchedClaim, cancellationToken);
            }
            await _session.SaveChangesAsync(cancellationToken);
        }

        public override async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var exists = Users.Any(u => u.Id.Equals(user.Id));

            if (!exists)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserNotExist",
                        Description = $"User with id {user.Id} does not exists!"
                    }
                );
            }

            user.ConcurrencyStamp = Guid.NewGuid().ToString("N");
            await _session.AttachAsync(user, cancellationToken);
            await _session.SaveChangesAsync();
            return IdentityResult.Success;
        }

        protected override async Task AddUserTokenAsync(UserToken token)
        {
            ThrowIfDisposed();
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            await _session.AddAsync(token);
            await _session.SaveChangesAsync();
        }

        protected override Task<Role> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            Role role = Roles.FirstOrDefault(r => r.NormalizedName == normalizedRoleName);

            return Task.FromResult(role);
        }

        protected override Task<UserToken> FindTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            UserToken token = UserTokens.FirstOrDefault(
                ut => ut.UserId.Equals(user.Id) &&
                    ut.LoginProvider == loginProvider
                    && ut.Name == name);

            return Task.FromResult(token);
        }

        protected override async Task<User> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            User user = await _session.FindAsync<User>(userId, cancellationToken);
            return user;
        }

        protected override Task<UserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            UserLogin userLogin = UserLogins.FirstOrDefault(
                ul => ul.UserId.Equals(userId) && ul.LoginProvider == loginProvider
                    && ul.ProviderKey == providerKey
            );
            return Task.FromResult(userLogin); ;
        }

        protected override Task<UserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            UserLogin userLogin = UserLogins.FirstOrDefault(
                ul => ul.LoginProvider == loginProvider
                    && ul.ProviderKey == providerKey
            );
            return Task.FromResult(userLogin);
        }

        protected override Task<UserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            UserRole userRole = UserRoles.FirstOrDefault(
                ur => ur.UserId.Equals(userId) && ur.RoleId.Equals(roleId)
            );

            return Task.FromResult(userRole);
        }

        protected override async Task RemoveUserTokenAsync(UserToken token)
        {
            ThrowIfDisposed();
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            await _session.RemoveAsync(token);
            await _session.SaveChangesAsync();
        }
    }
}
