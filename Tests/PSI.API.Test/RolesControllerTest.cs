
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
        [TestMethod]
        public async Task IntegrationTesting()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();
            var response = await httpClient.PostAsJsonAsync(URI, new Role { Id = 1, Name = "Admins" });

            Assert.IsTrue(response.IsSuccessStatusCode);

            response = await httpClient.GetAsync($"{URI}/RoleExists/Admins");

            Assert.IsTrue(response.IsSuccessStatusCode);

            bool result = await response.Content.ReadFromJsonAsync<bool>();

            Assert.IsTrue(result);

            response = await httpClient.GetAsync($"{URI}/1");

            Assert.IsTrue(response.IsSuccessStatusCode);

            Role role = await response.Content.ReadFromJsonAsync<Role>();

            Assert.AreEqual(1, role.Id);

            response = await httpClient.PatchAsync($"{URI}", JsonContent.Create(new Role { Id = 1, Name = "patch" }));

            Assert.IsTrue(response.IsSuccessStatusCode);

            response = await httpClient.PutAsJsonAsync($"{URI}", new Role { Id = 1, Name = "put" });

            Assert.IsTrue(response.IsSuccessStatusCode);

        }
    }
}