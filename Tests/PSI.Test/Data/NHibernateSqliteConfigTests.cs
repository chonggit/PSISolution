using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PSI.Administration.Identity;
using PSI.Data;

namespace PSI.Test.Data
{
    [TestClass]
    public class NHibernateSqliteConfigTests
    {
        private ServiceCollection services = new ServiceCollection();
        private ServiceProvider serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            services.AddNHibernate(config =>
            {
                config.UseSqlite("Data Source =PSISolution.db");
            });

            serviceProvider = services.BuildServiceProvider();

            new SchemaExport(serviceProvider.GetService<Configuration>()).Create(false, true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            serviceProvider.Dispose();
        }

        /// <summary>
        /// 是否有注册 NHibernate 相关的类型
        /// </summary>
        [TestMethod]
        public void IsRegisterTypes()
        {
            Configuration configuration = serviceProvider.GetService<Configuration>();

            Assert.IsNotNull(configuration);

            ISessionFactory sessionFactory = serviceProvider.GetService<ISessionFactory>();

            Assert.IsNotNull(sessionFactory);

            IDbSession dbSession = serviceProvider.GetService<IDbSession>();

            Assert.IsNotNull(dbSession);
        }

        [TestMethod]
        public void IdentityUserTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<User> users = dbSession.Query<User>();

            Assert.AreEqual(0, users.Count());

            dbSession.Add(new User
            {
                UserName = "username",
                AccessFailedCount = 1,
                ConcurrencyStamp = "stamp",
                Email = "email",
                EmailConfirmed = true,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "email",
                NormalizedUserName = "username",
                PasswordHash = "hash",
                PhoneNumber = "number",
                PhoneNumberConfirmed = true,
                SecurityStamp = "security",
                TwoFactorEnabled = true
            }); ;

            dbSession.SaveChanges();

            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void IdentityRoleTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<Role> roles = dbSession.Query<Role>();

            Assert.AreEqual(0, roles.Count());

            dbSession.Add(new Role { Name = "role", NormalizedName = "ROLE", ConcurrencyStamp = "stamp" });

            dbSession.SaveChanges();

            Assert.AreEqual(1, roles.Count());
        }

        [TestMethod]
        public void IdentityRoleClaimTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<RoleClaim> roleClaims = dbSession.Query<RoleClaim>();

            Assert.AreEqual(0, roleClaims.Count());

            dbSession.Add(new RoleClaim { ClaimType = "claimType", ClaimValue = "claimValue", RoleId = 1 });
            dbSession.SaveChanges();

            Assert.AreEqual(1, roleClaims.Count());
        }

        [TestMethod]
        public void IdentityUserClaimTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<UserClaim> userClaims = dbSession.Query<UserClaim>();

            Assert.AreEqual(0, userClaims.Count());

            dbSession.Add(new UserClaim { ClaimType = "claimType", ClaimValue = "claimValue", UserId = 1 });
            dbSession.SaveChanges();

            Assert.AreEqual(1, userClaims.Count());

        }

        [TestMethod]
        public void IdentityUserLoginTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<UserLogin> userLogins = dbSession.Query<UserLogin>();

            Assert.AreEqual(0, userLogins.Count());

            dbSession.Add(new UserLogin { ProviderKey = "key", LoginProvider = "loginProvider", ProviderDisplayName = "displayName", UserId = 1 });
            dbSession.SaveChanges();

            Assert.AreEqual(1, userLogins.Count());
        }

        [TestMethod]
        public void IdentityUserRoleTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<UserRole> userRoles = dbSession.Query<UserRole>();

            Assert.AreEqual(0, userRoles.Count());

            dbSession.Add(new UserRole { RoleId = 1, UserId = 1 });
            dbSession.SaveChanges();

            Assert.AreEqual(1, userRoles.Count());
        }

        [TestMethod]
        public void IdentityUserTokenTest()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<UserToken> userTokens = dbSession.Query<UserToken>();

            Assert.AreEqual(0, userTokens.Count());

            dbSession.Add(new UserToken { LoginProvider = "provider", Name = "name", UserId = 1, Value = "value" });
            dbSession.SaveChanges();

            Assert.AreEqual(1, userTokens.Count());
        }
    }
}