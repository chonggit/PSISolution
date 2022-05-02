using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using PSI.Inventory;

namespace PSI.NHibernate.Test
{
    [TestClass]
    public class NHibernateExtensionsTests
    {
        private const string CONNECTION_STRING = "Data Source=PSISolution.db";

        /// <summary>
        /// 创建数据库架构
        /// </summary>
        public void CreateSchema()
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
            CreateSchema();

            var services = new ServiceCollection();

            services.AddNHibernate(db =>
            {
                db.UseSqlite(CONNECTION_STRING);
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (ISession session = serviceProvider.GetRequiredService<ISession>())
            {
                session.Save(new Items { ItemCode = "itemCode", ItemName = "itemName" });
                session.Flush();

                Items items = session.Get<Items>("itemCode");

                Assert.AreEqual("itemCode", items.ItemCode);
                Assert.AreEqual("itemName", items.ItemName);
            }
        }
    }
}