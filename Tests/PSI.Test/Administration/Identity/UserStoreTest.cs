using Microsoft.AspNetCore.Identity;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PSI.Administration.Identity;
using PSI.Data;

namespace PSI.Test.Administration.Identity
{
    [TestClass]
    public class UserStoreTest
    {
        UserStore userStore = null;
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
        }
    }
}
