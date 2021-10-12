using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using ZipPayDemo.Application.Users.Command.CreateUser;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;
using ZipPayDemo.Tests.Fixtures;
using ZipPayDemo.Tests.Fixtures.TestClasses;

namespace ZipPayDemo.Tests.UnitTests.Commands
{
    public class CreateUserCommandHandlerTests : AutoMockTestsClass<CreateUserCommandHandler>
    {
        public CreateUserCommandHandlerTests(CommonTestsFixture testsFixture) : base(testsFixture)
        {
        }

        [Fact]
        public async Task Given_Command_Should_CreateUser()
        {
            // Arrange
            var command = Fixture.Create<CreateUserCommand>();

            // Act
            var actual = await Target.Handle(command, CancellationToken);

            // Assert
            actual.Should().BeOfType<CreateUserResponse>();
            Mock<IUserService>().Verify(x => x.CreateUserAsync(It.IsAny<User>()), Times.Once);

        }
    }
}
