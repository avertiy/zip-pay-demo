using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using Xunit;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Services;
using ZipPayDemo.Tests.Fixtures;

namespace ZipPayDemo.Tests.UnitTests.Services
{
    public class UserServiceTests : AutoMockTestsClass<UserService>
    {
        private readonly List<User> _users = new List<User>()
        {
            new User() { Email = "user1@email.com",MonthlySalary = 1500, MonthlyExpenses = 500, Name = "user1", Id = 1,},
            new User() { Email = "user2@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user2", Id = 2},
            new User() { Email = "user3@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user3", Id = 3},
        };
        public UserServiceTests(CommonTestsFixture testsFixture) : base(testsFixture)
        {
        }
        

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 2)]
        public async Task GetAllAsync_Should_Return_Users(int skip, int take)
        {
            //arrange
            Mock<IRepository>()
                .Setup(s => s.GetListAsync(It.IsAny<Specification<User>>(), CancellationToken))
                .ReturnsAsync(_users.Skip(skip).Take(take).ToList());
            
            //act 
            var users = await Target.GetAllAsync(skip, take);

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
            Mock<IRepository>()
                .Setup(s => s.GetListAsync(It.IsAny<Specification<User>>(), CancellationToken))
                .ReturnsAsync(_users);

            //act - assert 
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(()=> Target.GetAllAsync(0, 0));
        }

        [Fact]
        public async Task GetByIdAsync_returns_user()
        {
            //arrange
            Mock<IRepository>()
                .Setup(s => s.GetByIdAsync<User>(It.IsAny<object>(), CancellationToken))
                .ReturnsAsync(_users.First());

            //act 
            var user = await Target.GetByIdAsync(1);

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
            Mock<IRepository>().Setup(s => 
                s.GetAsync(It.IsAny<Expression<Func<User,bool>>>(), CancellationToken))
                .ReturnsAsync(_users.FirstOrDefault(u => u.Email == email));

            //act 
            var user = await Target.GetUserByEmailAsync(email);

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
