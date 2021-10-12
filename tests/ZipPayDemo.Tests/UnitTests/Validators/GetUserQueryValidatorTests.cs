using FluentValidation.TestHelper;
using Xunit;
using ZipPayDemo.Application.Users.Query.GetUser;
using ZipPayDemo.Tests.Fixtures.TestClasses;

namespace ZipPayDemo.Tests.UnitTests.Validators
{
    public class GetUserQueryValidatorTests : ValidatorTestsClass<GetUserQueryValidator>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Ensure_UserId_GreaterThanZero(long userId)
        {
            // Arrange
            var query = new GetUserQuery() { UserId = userId };

            // Act
            var result = Target.TestValidate(query);

            // Assert

            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Theory]
        [InlineData(1)]
        public void Should_Be_Valid(long userId)
        {
            // Arrange
            var query = new GetUserQuery() { UserId = userId };

            // Act
            var result = Target.TestValidate(query);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
