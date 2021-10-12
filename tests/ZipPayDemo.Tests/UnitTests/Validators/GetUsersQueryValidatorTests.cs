using FluentValidation.TestHelper;
using Xunit;
using ZipPayDemo.Application.Users.Query.GetUsers;
using ZipPayDemo.Tests.Fixtures.TestClasses;

namespace ZipPayDemo.Tests.UnitTests.Validators
{
    public class GetUsersQueryValidatorTests : ValidatorTestsClass<GetUsersQueryValidator>
    {
        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void Should_Ensure_Take_Is_Valid(int take, int skip)
        {
            // Arrange
            var query = new GetUsersQuery() { Take = take, Skip = skip };

            // Act
            var result = Target.TestValidate(query);

            // Assert

            result.ShouldHaveValidationErrorFor(x => x.Take);
            result.ShouldHaveValidationErrorFor(x => x.Skip);
        }

        [Theory]
        [InlineData(10, 0)]
        public void Should_Be_Valid(int take, int skip)
        {
            // Arrange
            var query = new GetUsersQuery() { Take = take, Skip = skip };

            // Act
            var result = Target.TestValidate(query);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}