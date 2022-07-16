using Microsoft.AspNetCore.Identity;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PSI.Administration.Identity;
using PSI.Data;
using System.Security.Claims;

namespace PSI.Test.Administration.Identity
{
    [TestClass]
    public class UserStoreTest
    {
        UserStore userStore = null;
        RoleStore roleStore = null;
        ServiceProvider serviceProvider = null;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddNHibernate(config =>
            {
                config.UseSqlite("Data Source =PSISolution.db");
            });
            serviceProvider = services.BuildServiceProvider();

            new SchemaExport(serviceProvider.GetService<Configuration>()).Create(false, true);

            IDbSession session = services.BuildServiceProvider().GetRequiredService<IDbSession>();

            userStore = new UserStore(session, new IdentityErrorDescriber());
            roleStore = new RoleStore(session, new IdentityErrorDescriber());
        }

        [TestCleanup]
        public void Cleanup()
        {
            serviceProvider.Dispose();
            serviceProvider = null;
            userStore = null;
        }

        [TestMethod]
        public void QueryAllTest()
        {
            IEnumerable<User> users = userStore.Users.ToList();

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task CanDoCURDAsync()
        {
            var user = new User
            {
                UserName = "username",
                AccessFailedCount = 1,
                Email = "email",
                EmailConfirmed = true,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "EMAIL",
                NormalizedUserName = "USERNAME",
                PasswordHash = "hash",
                PhoneNumber = "number",
                PhoneNumberConfirmed = true,
                SecurityStamp = "security",
                TwoFactorEnabled = true
            };

            var result = await userStore.CreateAsync(user);

            Assert.IsTrue(result.Succeeded);
            Assert.IsFalse(string.IsNullOrEmpty(user.ConcurrencyStamp));

            user.UserName = "username1";
            user.NormalizedUserName = user.UserName.ToUpperInvariant();

            result = await userStore.UpdateAsync(user);

            Assert.IsTrue(result.Succeeded);
            Assert.IsFalse(string.IsNullOrEmpty(user.ConcurrencyStamp));

            var foundUser = await userStore.FindByIdAsync(user.Id.ToString());

            Assert.AreEqual(user.Id, foundUser.Id);
            Assert.AreEqual(user.UserName, foundUser.UserName);

            foundUser = await userStore.FindByNameAsync(user.NormalizedUserName);

            Assert.AreEqual(user.Id, foundUser.Id);
            Assert.AreEqual(user.NormalizedUserName, foundUser.NormalizedUserName);

            foundUser = await userStore.FindByEmailAsync(user.NormalizedEmail);

            Assert.AreEqual(user.Id, foundUser.Id);
            Assert.AreEqual(user.NormalizedEmail, foundUser.NormalizedEmail);

            await userStore.AddLoginAsync(user, new UserLoginInfo("provider", "key", "displayname"));

            foundUser = await userStore.FindByLoginAsync("provider", "key");

            Assert.AreEqual(user.Id, foundUser.Id);

            await userStore.RemoveLoginAsync(user, "provider", "key");
            foundUser = await userStore.FindByLoginAsync("provider", "key");

            Assert.IsNull(foundUser);

            await userStore.AddClaimsAsync(user, new List<Claim> { new Claim("name", "username1") });
            IEnumerable<Claim> claims = await userStore.GetClaimsAsync(user);

            Assert.AreEqual(1, claims.Count());

            await userStore.RemoveClaimsAsync(user, new List<Claim> { new Claim("name", "username1") });
            claims = await userStore.GetClaimsAsync(user);

            Assert.AreEqual(0, claims.Count());

            await roleStore.CreateAsync(new Role { Name = "name", NormalizedName = "NAME" });
            await userStore.AddToRoleAsync(user, "NAME");

            bool isInRole = await userStore.IsInRoleAsync(user, "NAME");

            Assert.IsTrue(isInRole);

            await userStore.RemoveFromRoleAsync(user, "NAME");
            isInRole = await userStore.IsInRoleAsync(user, "NAME");

            Assert.IsFalse(isInRole);

            result = await userStore.DeleteAsync(user);

            Assert.IsTrue(result.Succeeded);
        }
    }
}
