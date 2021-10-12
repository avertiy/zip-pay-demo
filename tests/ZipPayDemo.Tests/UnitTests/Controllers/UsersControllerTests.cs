using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Moq;
using TestProject.WebAPI.Models;
using Xunit;
using ZipPayDemo.Api.Controllers;
using ZipPayDemo.Application.Users.Command.CreateUser;
using ZipPayDemo.Application.Users.Query.GetUser;
using ZipPayDemo.Application.Users.Query.GetUsers;
using ZipPayDemo.Tests.Fixtures;
using ZipPayDemo.Tests.Fixtures.TestClasses;
using ZipPayDemo.Tests.Utilities.Extensions;

namespace ZipPayDemo.Tests.UnitTests.Controllers
{
    public class UsersControllerTests : AutoMockTestsClass<UsersController>
    {
        public UsersControllerTests(CommonTestsFixture testsFixture) : base(testsFixture)
        {
        }

        [Fact]
        public async Task GetUsersQuery_Should_Return_Users_OkResult()
        {
            // Arrange
            var query = Fixture.Create<GetUsersQuery>();
            Mock<IMediator>()
                .Setup(x => x.Send(query, default))
                .ReturnsAsync(new GetUsersResponse { Users = new List<UserModel>() { new UserModel() }});

            // Act
            var actual = await Target.Index(query);

            // Assert
            await actual.Should().BeOkResult<GetUsersResponse>(x =>
            {
                x.Users.Should().AllBeOfType<UserModel>();
            });
        }

        [Fact]
        public async Task GetUserQuery_Should_Return_User_OkResult()
        {
            // Arrange
            var query = Fixture.Create<GetUserQuery>();
            Mock<IMediator>()
                .Setup(x => x.Send(query, default))
                .ReturnsAsync(new GetUserResponse { User = new UserModel() });

            // Act
            var actual = await Target.GetById(query);

            // Assert
            await actual.Should().BeOkResult<GetUserResponse>(x =>
            {
                x.User.Should().BeOfType<UserModel>();
            });
        }

        [Fact]
        public async Task CreateUserCommand_Should_Return_Status_201()
        {
            // Arrange
            var command = Fixture.Create<CreateUserCommand>();
            Mock<IMediator>()
                .Setup(x => x.Send(command, default))
                .ReturnsAsync(new CreateUserResponse { Success = true });

            // Act
            var actual = await Target.Create(command);

            // Assert
            await actual.Should().BeObjectResult<CreateUserResponse>(HttpStatusCode.Created);
        }
    }
}
