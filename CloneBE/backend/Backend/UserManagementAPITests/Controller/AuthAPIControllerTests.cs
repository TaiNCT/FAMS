using AutoMapper;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using UserManagementAPI.Controllers;
using UserManagementAPI.Models.DTO;
using UserManagementAPI.Services;
using UserManagementAPITests.UserData;

namespace UserManagementAPITests.Controller
{
    public class AuthAPIControllerTests
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthenticationAPIController _controller;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IEmailService> _mailServiceMock;
        public AuthAPIControllerTests()
        {
            //Mock db
            _dbContextMock = new Mock<FamsContext>();
            _mapperMock = new Mock<IMapper>();
            _configuration = new Mock<IConfiguration>();
            _mailServiceMock = new Mock<IEmailService>();
            //Mock data to db
            var usersDemo = UserMockData.GetUsers().AsQueryable();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(usersDemo.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(usersDemo.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(usersDemo.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(usersDemo.GetEnumerator());
            _dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

            //Mock mapper 
            var mapper = ConfigurationExtension.ConfigureAutoMapper();

            // Mock the configuration to return a test token
            _configuration.Setup(c => c.GetSection("AppSettings:Token").Value).Returns("this is my custom Secret key for authentication");

            //SUT
            _controller = new AuthenticationAPIController(_dbContextMock.Object, mapper, _configuration.Object, _mailServiceMock.Object);
        }

        [Fact]
        public void AuthAPIController_LoginUser_ReturnsOk()
        {
            //Arragne
            var loginDto = new LoginDTO { username = "myphuong", password = "admin@123456" };

            // Act
            var result = _controller.LoginUser(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result);
            var responseDto = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.True(responseDto.IsSuccess);
            Assert.Equal("OK", responseDto.Message);
        }

        [Fact]
        public void AuthAPIController_LoginUser_WrongUserName_ReturnsBadRequest()
        {
            //Arrange
            var loginDto = new LoginDTO { username = "myphuon", password = "admin@123456" };

            // Act
            var result = _controller.LoginUser(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result);
            var responseDto = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("Cannot found the user.", responseDto.Message);
        }

        [Fact]
        public void AuthAPIController_LoginUser_WrongPassword_ReturnsBadRequest()
        {
            //Arrange
            var loginDto = new LoginDTO { username = "myphuong", password = "admin@12345" };

            //Act
            var result = _controller.LoginUser(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result);
            var responseDto = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("The password is not match.", responseDto.Message);
        }
    }
}

