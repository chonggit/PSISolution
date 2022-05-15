
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSI.Administration.Identity;
using PSI.Data;
using PSI.EntityFramework;
using System.Security.Claims;

namespace PSI.Test.Administration.Identity
{
    [TestClass]
    public class RoleStoreTest
    {
        RoleStore roleStore = null;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework(options => options.UseInMemoryDatabase("PSISolution"));

            IDbSession session = services.BuildServiceProvider().GetRequiredService<IDbSession>();

            roleStore = new RoleStore(session, new IdentityErrorDescriber());
        }

        [TestMethod]
        public void QueryAllTest()
        {
            var roles = roleStore.Roles.ToList();

            Assert.IsNotNull(roles);
        }

        [TestMethod]
        public async Task CanDoCURD()
        {
            var role = new Role
            {
                Id = 1,
                Name = "Role 1"
            };
            role.NormalizedName = role.Name.ToUpperInvariant();

            var result = await roleStore.CreateAsync(role, CancellationToken.None);
            Assert.IsTrue(result.Succeeded);

            Assert.IsFalse(string.IsNullOrEmpty(role.ConcurrencyStamp));

            role.Name = "role 1 updated";
            role.NormalizedName = role.Name.ToUpperInvariant();
            result = await roleStore.UpdateAsync(role, CancellationToken.None);
            Assert.IsTrue(result.Succeeded);

            role = await roleStore.FindByIdAsync(role.Id.ToString(), CancellationToken.None);
            Assert.AreEqual(role.Id, role.Id);
            Assert.AreEqual(role.Name, role.Name);

            var normalizedName = role.NormalizedName;
            role = await roleStore.FindByNameAsync(normalizedName, CancellationToken.None);
            Assert.AreEqual(normalizedName, role.NormalizedName);

            var claim = new Claim("test", "test");

            await roleStore.AddClaimAsync(role, claim, CancellationToken.None);

            var roleClaims = await roleStore.GetClaimsAsync(role, CancellationToken.None);
            Assert.IsNotNull(roleClaims);
            Assert.IsTrue(roleClaims.Count > 0);

            await roleStore.RemoveClaimAsync(role, claim, CancellationToken.None);

            roleClaims = await roleStore.GetClaimsAsync(role, CancellationToken.None);
            Assert.IsNotNull(roleClaims);
            Assert.IsTrue(roleClaims.Count == 0);

            result = await roleStore.DeleteAsync(role, CancellationToken.None);
            Assert.IsTrue(result.Succeeded);
        }
    }
}