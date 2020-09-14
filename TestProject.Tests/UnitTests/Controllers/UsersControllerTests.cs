using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestProject.WebAPI.Contracts;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Data.Entities;
using TestProject.WebAPI.Models;
using Xunit;

namespace TestProject.Tests.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly List<UserModel> _userModels;
        private readonly List<User> _users;
        public UsersControllerTests()
        {
            _users= new List<User>()
            {
                new User(){Email = "user1@email.com",MonthlySalary = 1500, MonthlyExpenses = 500, Name = "user1", Id = 1,},
                new User(){Email = "user2@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user2", Id = 2},
            };
            _userModels = new List<UserModel>()
            {
                new UserModel(){Email = "user1@email.com",MonthlySalary = 1500, MonthlyExpenses = 500, Name = "user1", Id = 1,},
                new UserModel(){Email = "user2@email.com",MonthlySalary = 2000, MonthlyExpenses = 500, Name = "user2", Id = 2},
            };
        }

        [Fact]
        public async Task Test_index_success()
        {
            //Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(_users);

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            

            mapperMock.Setup(m => m.Map<IList<UserModel>>(It.IsAny<List<User>>()))
                .Returns(_userModels);
            
            var controller = new UsersController(
                userServiceMock.Object,
                mapperMock.Object);

            //Act
            IActionResult actionResult = await controller.Index(0, 20);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(_userModels, result.Value);
        }

        [Fact]
        public async Task Test_index_must_return_bad_request()
        {
            //Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(_users);

            Mock<IMapper> mapperMock = new Mock<IMapper>();


            mapperMock.Setup(m => m.Map<IList<UserModel>>(It.IsAny<List<User>>()))
                .Returns(_userModels);

            var controller = new UsersController(
                userServiceMock.Object,
                mapperMock.Object);

            //Act
            IActionResult actionResult = await controller.Index(0, 0);
            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Test_getById_success()
        {
            //Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetByIdAsync(It.IsAny<long>())).
                ReturnsAsync(_users.First());

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<User>()))
                .Returns(_userModels.First());

            var controller = new UsersController(
                userServiceMock.Object,
                mapperMock.Object);

            //Act
            IActionResult actionResult = await controller.GetById(1);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(_userModels.First(), result.Value);
        }

        [Fact]
        public async Task Test_getById_must_return_not_found()
        {
            //Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetByIdAsync(It.IsAny<long>())).
                ReturnsAsync((User) null);

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            var controller = new UsersController(
                userServiceMock.Object,
                mapperMock.Object);

            //Act
            IActionResult actionResult = await controller.GetById(3);

            //assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal((int)StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Test_create_success()
        {
            //Arrange
            var createUserModel = new CreateUserModel()
                {Email = "user@mail.com", Name = "user", MonthlySalary = 100, MonthlyExpenses = 50};
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).
                ReturnsAsync(_users.First());

            userServiceMock.Setup(m => m.GetUserByEmailAsync(It.Is<string>(email => createUserModel.Email == email))).
                ReturnsAsync((User)null);
            

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<User>(It.IsAny<CreateUserModel>()))
                .Returns(_users.First());

            mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<User>()))
                .Returns(_userModels.First());

            var controller = new UsersController(
                userServiceMock.Object,
                mapperMock.Object);

            //Act
            IActionResult actionResult = await controller.Create(createUserModel);

            //assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
