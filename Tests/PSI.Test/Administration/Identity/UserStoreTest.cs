using Microsoft.AspNetCore.Identity;
using PSI.Administration.Identity;
using PSI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // services.AddEntityFramework(options => options.UseInMemoryDatabase("PSISolution"));
            serviceProvider = services.BuildServiceProvider();

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
    }
}
