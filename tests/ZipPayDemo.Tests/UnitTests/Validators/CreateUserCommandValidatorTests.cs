using FluentValidation.TestHelper;
using Moq;
using Xunit;
using ZipPayDemo.Application.Users.Command.CreateUser;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;
using ZipPayDemo.Tests.Fixtures.TestClasses;

namespace ZipPayDemo.Tests.UnitTests.Validators
{
    public class CreateUserCommandValidatorTests : ValidatorTestsClass<CreateUserCommandValidator>
    {
        [Theory]
        [InlineData("test")]
        [InlineData("")]
        [InlineData("user.exists@mail.com")]
        public void Given_Email_Should_Be_InValid(string email)
        {
            // Arrange
            var query = new CreateUserCommand() { Email = email };
            Mock<IUserService>().Setup(x => x.GetUserByEmailAsync(It.Is<string>(m => m == "user.exists@mail.com"))).ReturnsAsync(new User());
            // Act
            var result = Target.TestValidate(query);

            // Assert

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        public void Given_Name_Should_Be_InValid(string name)
        {
            // Arrange
            var query = new CreateUserCommand() { Email = "test@mail.com", Name = name };
            
            // Act
            var result = Target.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_Salary_Should_Be_InValid(decimal salary)
        {
            // Arrange
            var query = new CreateUserCommand() { Email = "test@mail.com", Name = "test", MonthlySalary = salary };

            // Act
            var result = Target.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MonthlySalary);
        }

        [Theory]
        [InlineData("test@mail.com", "test", 10, 20)]
        public void Should_Be_Valid(string email, string name, decimal expenses, decimal salary)
        {
            // Arrange
            var query = new CreateUserCommand() { Email = email, Name = name, MonthlyExpenses = expenses, MonthlySalary = salary };

            // Act
            var result = Target.TestValidate(query);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}