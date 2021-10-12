using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;
using ZipPayDemo.Application.Users.Command.CreateUser;
using ZipPayDemo.Application.Users.Query.GetUser;
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

        [Fact]
        public async Task GetUserById_Test200Status()
        {
            await Test200OkResult<GetUserResponse>("/api/users/1", x =>
            {
                Assert.NotNull(x.User);
                Assert.Equal(1, x.User.Id);
            });
        }

        [Fact]
        public async Task Can_Create_User()
        {
            var request = new TestRequest("/api/users",
                new CreateUserCommand() { Name = "test", Email = "user5@mail.com", MonthlySalary = 1000, MonthlyExpenses = 100 });
            await Test200OkResult<CreateUserResponse>(request, x =>
            {
                Assert.True(x.Success);
            });
        }

    }
}