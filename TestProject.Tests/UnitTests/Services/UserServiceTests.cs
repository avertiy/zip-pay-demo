using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using TestProject.WebAPI.Data.Entities;
using TestProject.WebAPI.Services;
using Xunit;

namespace TestProject.Tests.UnitTests.Services
{
    public class UserServiceTests
    {
        List<User> _users = new List<User>()
        {
            new User() { Email = "user1@email.com",MonthlySalary = 1500, MonthlyExpenses = 500, Name = "user1", Id = 1,},
            new User() { Email = "user2@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user2", Id = 2},
            new User() { Email = "user3@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user3", Id = 3},
        };

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 2)]
        public async Task GetAllAsync_returns_users(int skip, int take)
        {
            //arrange
            var repository = new Mock<IRepository>();
            repository.Setup(s => s.GetEntityListAsync(It.IsAny<Specification<User>>(), It.IsAny<bool>())).ReturnsAsync(_users);
            var userService = new UserService(repository.Object);
            
            //act 
            var users = await userService.GetAllAsync(skip, take);

            //assert
            if (skip > 0)
            {
                Assert.Contains(users, u => u.Name == "user3");
            }
            else
            {
                Assert.Contains(users, u => u.Name == "user1");
                Assert.Contains(users, a => a.Name == "user2");
            }
        }

        [Fact]
        public async Task GetAllAsync_when_take_invalid_throw_exception()
        {
            //arrange
            var repository = new Mock<IRepository>();
            repository.Setup(s => s.GetEntityListAsync(It.IsAny<Specification<User>>(), It.IsAny<bool>())).ReturnsAsync(_users);
            var userService = new UserService(repository.Object);

            //act - assert 
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(()=> userService.GetAllAsync(0, 0));
        }

        [Fact]
        public async Task GetByIdAsync_returns_user()
        {
            //arrange
            var repository = new Mock<IRepository>();
            repository.Setup(s => s.GetEntityByIdAsync<User>(It.IsAny<object>(), It.IsAny<bool>())).ReturnsAsync(_users.First());
            var userService = new UserService(repository.Object);

            //act 
            var user = await userService.GetByIdAsync(1);

            //assert
            Assert.NotNull(user);
            Assert.Equal(user.Name, "user1");
        }

        [Theory]
        [InlineData("user1@email.com")]
        [InlineData("user2@email.com")]
        [InlineData("")]
        public async Task GetUserByEmailAsync_returns_user(string email)
        {
            //arrange
            var repository = new Mock<IRepository>();
            
            repository.Setup(s => 
                s.GetEntityAsync<User>(It.IsAny<Expression<Func<User,bool>>>(), It.IsAny<bool>()))
                .ReturnsAsync(_users.FirstOrDefault(u => u.Email == email));

            var userService = new UserService(repository.Object);

            //act 
            var user = await userService.GetUserByEmailAsync(email);

            //assert
            if (string.IsNullOrEmpty(email))
            {
                Assert.Null(user);
            }
            else
            {
                Assert.NotNull(user);
                Assert.Equal(user.Email, email);
            }
        }
    }
}
