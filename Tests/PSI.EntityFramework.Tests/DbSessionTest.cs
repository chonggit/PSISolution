using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.EntityFramework.Tests
{
    [TestClass]
    public class DbSessionTest
    {
        DbSession dbSession;
        DbContext dbContext = new TestDbContext();

        [TestInitialize]
        public void Setup()
        {
            dbSession = new DbSession(dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbSession.Dispose();
        }

        [TestMethod]
        public void AddTest()
        {
            TestEntity entity = dbSession.Add(new TestEntity { Key = "key1", Name = "name1" });

            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key1");

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity1);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            TestEntity entity = await dbSession.AddAsync(new TestEntity { Key = "key2", Name = "name2" });

            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key2");

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            dbSession.Add(new TestEntity { Key = "key13" });
            dbSession.SaveChanges();

            TestEntity entity = dbSession.Find<TestEntity>("key13");

            entity.Name = "name update";

            dbSession.Update(entity);
            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key13");

            Assert.AreEqual("name update", entity1.Name);
        }

        [TestMethod]
        public async Task UpdateAsync()
        {
            dbSession.Add(new TestEntity { Key = "key12" });
            dbSession.SaveChanges();

            TestEntity entity = dbSession.Find<TestEntity>("key12");

            entity.Name = "name update";

            await dbSession.UpdateAsync(entity);

            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key12");

            Assert.AreEqual("name update", entity1.Name);
        }

        [TestMethod]
        public void FindTest()
        {
            dbSession.Add(new TestEntity { Key = "key10" });
            dbSession.SaveChanges();

            TestEntity entity = dbSession.Find<TestEntity>("key10");

            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public async Task FindAsyncTest()
        {
            dbSession.Add(new TestEntity { Key = "key11" });
            dbSession.SaveChanges();

            TestEntity entity = await dbSession.FindAsync<TestEntity>("key11");

            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void RemoveTest()
        {
            TestEntity entity = dbSession.Add(new TestEntity { Key = "removeKey1" });
            dbSession.SaveChanges();

            dbSession.Remove(entity);
            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("removeKey1");

            Assert.IsNull(entity1);
        }

        [TestMethod]
        public async Task RemoveAsyncTest()
        {
            TestEntity entity = dbSession.Add(new TestEntity { Key = "removeKey1" });
            dbSession.SaveChanges();

            await dbSession.RemoveAsync(entity);
            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("removeKey1");

            Assert.IsNull(entity1);
        }

        [TestMethod]
        public void AttachTest()
        {
            var entity = dbContext.Add(new TestEntity { Key = "key31" });

            dbContext.SaveChanges();

            entity.State = EntityState.Detached;

            dbSession.Attach(new TestEntity { Key = "key31", Name = "attactname" });
            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key31");

            Assert.AreEqual("attactname", entity1.Name);
        }

        [TestMethod]
        public async Task AttachAsyncTest()
        {
            var entity = dbContext.Add(new TestEntity { Key = "key32" });

            dbContext.SaveChanges();

            entity.State = EntityState.Detached;

            await dbSession.AttachAsync(new TestEntity { Key = "key32", Name = "attactname" });
            dbSession.SaveChanges();

            TestEntity entity1 = dbSession.Find<TestEntity>("key32");

            Assert.AreEqual("attactname", entity1.Name);
        }

        public class TestDbContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("PSISolution");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var builder = modelBuilder.Entity<TestEntity>();

                builder.HasKey(x => x.Key);
                builder.Property(x => x.Key).HasMaxLength(20);
                builder.Property(x => x.Name).HasMaxLength(100);
            }
        }

        public class TestEntity
        {
            public string Key { get; set; }
            public string Name { get; set; }
        }
    }
}
