
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using PSI.Administration.Identity;
using PSI.Data;

namespace PSI.API.Test
{
    [TestClass]
    public class RolesControllerTest
    {
        private const string URI = "v1/roles";

        private HttpClient client = null;
        private readonly AutoMocker mocker = new AutoMocker();

        [TestInitialize]
        public void Setup()
        {
            var application = new WebApplicationFactory<Program>()
                  .WithWebHostBuilder(builder =>
                  {
                  });
            client = application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient(serviceProvider => mocker.GetMock<RoleManager<Role>>().Object);
                    services.AddTransient(ServiceProvider => mocker.GetMock<IDbSession>().Object);
                });
            }).CreateClient();
        }

        [TestMethod]
        public async Task GetRoleById()
        {
            mocker.Setup<RoleManager<Role>, Task<Role>>(setup => setup.FindByIdAsync(It.IsAny<string>()))
                .Returns<string>((id) => Task.FromResult(new Role { Id = int.Parse(id), Name = "findByIdRole", NormalizedName = "RoleToFind" }));

            HttpResponseMessage response = await client.GetAsync($"{URI}/1");
            Role role = await response.Content.ReadFromJsonAsync<Role>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(role);
            Assert.AreEqual(1, role.Id);
        }

        [TestMethod]
        public async Task GetRoles()
        {
            mocker.Setup<RoleManager<Role>, IQueryable<Role>>(setup => setup.Roles)
                .Returns(new List<Role> { new Role { } }.AsQueryable());

            HttpResponseMessage response = await client.GetAsync(URI);
            IEnumerable<Role> roles = await response.Content.ReadFromJsonAsync<IEnumerable<Role>>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(roles);
        }

        [TestMethod]
        public async Task AddRole()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(URI, new Role { Name = "addrole", NormalizedName = "name" });
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task DeleteRole()
        {
            HttpResponseMessage response = await client.DeleteAsync($"{URI}/1");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task PatchRole()
        {
            HttpResponseMessage response = await client.PatchAsync(URI, JsonContent.Create<Role>(new Role { Id = 1, Name = "name1", NormalizedName = "name2" }));
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task RoleExists()
        {
            HttpResponseMessage response = await client.GetAsync($"{URI}/RoleExists/test");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}