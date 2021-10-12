using System.Threading.Tasks;
using Xunit;
using ZipPayDemo.Application.Users.Query.GetUsers;
using ZipPayDemo.Tests.Fixtures.TestClasses;
using ZipPayDemo.Tests.IntegrationTests.Setup;

namespace ZipPayDemo.Tests.IntegrationTests.Controllers
{
    public class UserControllerIntegrationTests : IntegrationTestsClass
    {
        public UserControllerIntegrationTests(ApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetUsers_Test200Status()
        {
            await Test200OkResult<GetUsersResponse>("/api/users", x =>
            {
                Assert.Contains(x.Users, a => a.Name == "user1");
                Assert.Contains(x.Users, a => a.Name == "user2");
            });
        }

        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public async Task Can_Get_User_ById(int id)
        //{
        //    var httpResponse = await _client.GetAsync($"/api/users/{id}");

        //    httpResponse.EnsureSuccessStatusCode();
        //    var user = await httpResponse.ReadBody<UserModel>();

        //    Assert.NotNull(user);
        //    Assert.Equal(user.Id, id);
        //    Assert.Equal(user.Name, "user"+id);
        //}

        //[Fact]
        //public async Task Can_Create_User()
        //{
        //    var httpResponse = await _client.PostAsync("/api/users/",
        //        new CreateUserModel() { Name = "user5", Email = "user5@mail.com", MonthlySalary = 6000, MonthlyExpenses = 2000},
        //        new JsonMediaTypeFormatter());

        //    Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        //}

    }
}