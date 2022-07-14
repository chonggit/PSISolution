using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NHibernate;
using PSI.Inventory;
using PSI.Data;

namespace PSI.Test.Data
{
    [TestClass]
    public class DbSessionTest
    {
        ISessionFactory sessionFactory;
        ISession session;
        SchemaExport export;
        IDbSession dbSession;

        [TestInitialize]
        public void Setup()
        {
            var config = NHibernateExtensions.GetConfiguration(db => db.UseSqlite("Data Source=PSISolution.db"));

            sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            dbSession = null;//= new NHDbSession(session);
            export = new SchemaExport(config);
            export.Execute(true, true, false);

        }

        [TestCleanup]
        public void Cleanup()
        {
            sessionFactory.Dispose();
            export.Drop(true, true);
        }

        [TestMethod]
        public void AddTest()
        {
            dbSession.Add(new Items { ItemCode = "code", ItemName = "name" });
            dbSession.SaveChanges();

            Items item = dbSession.Find<Items>("code");

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            await dbSession.AddAsync(new Items { ItemCode = "code", ItemName = "name" });

            dbSession.SaveChanges();

            Items item = dbSession.Find<Items>("code");

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            item.ItemName = "updatename";
            dbSession.Update(item);
            dbSession.SaveChanges();

            Items item1 = dbSession.Find<Items>("code");

            Assert.AreEqual("updatename", item1.ItemName);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            item.ItemName = "updatename";
            await dbSession.UpdateAsync(item);
            dbSession.SaveChanges();

            Items item1 = dbSession.Find<Items>("code");

            Assert.AreEqual("updatename", item1.ItemName);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            dbSession.Remove(item);
            dbSession.SaveChanges();

            Items item1 = dbSession.Find<Items>("code");

            Assert.IsNull(item1);
        }

        [TestMethod]
        public async Task RemoveAsyncTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            await dbSession.RemoveAsync(item);
            dbSession.SaveChanges();

            Items item1 = dbSession.Find<Items>("code");

            Assert.IsNull(item1);
        }

        [TestMethod]
        public void AttachTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            session.Evict(item);
            item.ItemName = "attactname";
            dbSession.Attach(item);

            Items item1 = dbSession.Find<Items>("code");

            Assert.IsNotNull(item1);
            Assert.AreEqual("attactname", item1.ItemName);
        }

        [TestMethod]
        public async Task AttachAsyncTest()
        {
            var item = new Items { ItemCode = "code", ItemName = "name" };

            dbSession.Add(item);
            dbSession.SaveChanges();
            session.Evict(item);
            item.ItemName = "attactname";
            await dbSession.AttachAsync(item);

            Items item1 = dbSession.Find<Items>("code");

            Assert.IsNotNull(item1);
            Assert.AreEqual("attactname", item1.ItemName);
        }
    }
}
