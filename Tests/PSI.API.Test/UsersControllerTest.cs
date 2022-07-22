using Microsoft.AspNetCore.Identity;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PSI.Administration.Identity;
using System.Net;
using System.Text.Json;

namespace PSI.API.Test
{
    [TestClass]
    public class UsersControllerTest
    {
        private const string URI = "v1/users";

        [TestMethod]
        public async Task IntegrationTesting()
        {
            var application = new WebApplicationFactory<Program>();
            new SchemaExport(application.Services.GetRequiredService<Configuration>()).Create(false, true);
            var httpClient = application.CreateClient();
            await httpClient.PostAsJsonAsync("v1/roles", new Role { Name = "Role1" });
            await httpClient.PostAsJsonAsync("v1/roles", new Role { Name = "Role2" });
            await httpClient.PostAsJsonAsync("v1/roles", new Role { Name = "Role3" });
            var user = new User
            {
                UserName = "username",
                AccessFailedCount = 1,
                Email = "email",
                EmailConfirmed = true,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "EMAIL",
                NormalizedUserName = "USERNAME",
                PasswordHash = "hash",
                PhoneNumber = "number",
                PhoneNumberConfirmed = true,
                SecurityStamp = "security",
                TwoFactorEnabled = true
            };
            // 添加
            var response = await httpClient.PostAsJsonAsync(URI, user);

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            response = await httpClient.GetAsync($"{URI}/1");
            // 查询
            var foundUser = await response.Content.ReadFromJsonAsync<User>();

            Assert.AreEqual(user.UserName, foundUser.UserName);
            // 更新
            foundUser.UserName = "username1";
            response = await httpClient.PutAsJsonAsync(URI, foundUser);

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));
            // 添加到角色
            response = await httpClient.PostAsJsonAsync($"{URI}/Roles/1", new string[] { "Role1" });

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));

            // 添加到角色
            response = await httpClient.PostAsJsonAsync($"{URI}/Roles/1", new string[] { "Role2", "Role3" });

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));
            JsonContent.Create(new string[] { "Role1" });

            // 从角色删除
            response = await httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}{URI}/roles/1"),
                Content = JsonContent.Create(new string[] { "Role1" })
            });

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));

            // 从角色删除
            response = await httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}{URI}/roles/1"),
                Content = JsonContent.Create(new string[] { "Role2", "Role3" })
            });

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));

            response = await httpClient.DeleteAsync($"{URI}/1");

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(await GetIsSucceeded(response));
        }

        private async Task<bool> GetIsSucceeded(HttpResponseMessage response)
        {
            var result = await response.Content.ReadFromJsonAsync<JsonDocument>();
            result.RootElement.TryGetProperty("succeeded", out JsonElement value);
            return value.GetBoolean();
        }
    }
}
