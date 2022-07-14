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
        //private const string CONNECTION_STRING = "Data Source=:memory:";
        private const string CONNECTION_STRING = "Data Source = PSISolution.db";
        private ServiceCollection services = new ServiceCollection();
        private ServiceProvider serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            services.AddNHibernate(config =>
            {
                config.UseSqlite(CONNECTION_STRING);
            });

            serviceProvider = services.BuildServiceProvider();

            Configuration configuration = serviceProvider.GetService<Configuration>();
            new SchemaExport(configuration).Create(false, true);
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
        public void IdentityUserConfig()
        {
            IDbSession dbSession = serviceProvider.GetService<IDbSession>();
            IQueryable<User> users = dbSession.Query<User>();

            Assert.AreEqual(users.Count(), 0);

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

            Assert.AreEqual(users.Count(),1);
        }
    }
}