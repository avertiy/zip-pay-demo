using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using TestProject.Tests.Extensions;
using TestProject.Tests.IntegrationTests.Setup;
using TestProject.WebAPI.Models;
using Xunit;

namespace TestProject.Tests.IntegrationTests.Controllers
{
    public class UserControllerIntegrationTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTests(IntegrationTestsFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Can_Get_Users()
        {
            var httpResponse = await _client.GetAsync("/api/users");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var users = await httpResponse.ReadBody<IEnumerable<UserModel>>(); 
            
            Assert.NotNull(users);
            Assert.Contains(users, a => a.Name == "user1");
            Assert.Contains(users, a => a.Name == "user2");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Can_Get_User_ById(int id)
        {
            var httpResponse = await _client.GetAsync($"/api/users/{id}");

            httpResponse.EnsureSuccessStatusCode();
            var user = await httpResponse.ReadBody<UserModel>();

            Assert.NotNull(user);
            Assert.Equal(user.Id, id);
            Assert.Equal(user.Name, "user"+id);
        }

        [Fact]
        public async Task Can_Create_User()
        {
            var httpResponse = await _client.PostAsync("/api/users/",
                new CreateUserModel() { Name = "user5", Email = "user5@mail.com", MonthlySalary = 6000, MonthlyExpenses = 2000},
                new JsonMediaTypeFormatter());

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}