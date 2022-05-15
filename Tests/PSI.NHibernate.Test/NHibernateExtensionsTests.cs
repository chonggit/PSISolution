using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using PSI.Data;
using PSI.Inventory;

namespace PSI.NHibernate.Test
{
    [TestClass]
    public class NHibernateExtensionsTests
    {
        private const string CONNECTION_STRING = "Data Source=:memory:";

        /// <summary>
        /// 创建数据库架构
        /// </summary>
        [TestMethod]
        public void CreateSchemaTest()
        {
            Configuration configuration = NHibernateExtensions.GetConfiguration(db =>
            {
                db.ConnectionProvider<DriverConnectionProvider>();
                db.Driver<SQLite20Driver>();
                db.Dialect<SQLiteDialect>();
                db.ConnectionString = CONNECTION_STRING;
                db.LogSqlInConsole = true;
            });

            new SchemaExport(configuration).Create(false, true);
        }

        /// <summary>
        /// NHibernate SQLite
        /// </summary>
        [TestMethod]
        public void AddNHibernateTest()
        {
            var services = new ServiceCollection();

            services.AddNHibernate(db =>
            {
                db.UseSqlite(CONNECTION_STRING);
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IDbSession dbSession = serviceProvider.GetRequiredService<IDbSession>();

            Assert.IsNotNull(dbSession);
        }
    }
}