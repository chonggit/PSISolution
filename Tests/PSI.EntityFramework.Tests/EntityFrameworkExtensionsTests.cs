using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSI.Data;

namespace PSI.EntityFramework.Tests
{
    [TestClass]
    public class EntityFrameworkExtensionsTests
    {
        [TestMethod]
        public void AddEntityFrameworkTest()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework(optionsBuilder =>
            {
                optionsBuilder.UseInMemoryDatabase("PSISolution");
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            DbContext dbContext = serviceProvider.GetRequiredService<DbContext>();
            IDbSession dbSession = serviceProvider.GetRequiredService<IDbSession>();

            Assert.IsNotNull(dbContext);
            Assert.IsNotNull(dbSession);
        }
    }
}
