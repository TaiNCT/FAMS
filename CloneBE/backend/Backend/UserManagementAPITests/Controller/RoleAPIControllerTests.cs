using Azure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementAPI.Controllers;
using Entities.Models;
using UserManagementAPI.Models.DTO;
using UserManagementAPITests.UserData;
using Entities.Context;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPITests.Controller
{
    public class RoleAPIControllerTests
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly RoleAPIController _controller;
        public RoleAPIControllerTests()
        {
            _dbContextMock = new Mock<FamsContext>();

            //Mock data for Role in Db
            var role = RoleMockData.GetRoles().AsQueryable();
            var mockSetRole = new Mock<DbSet<Role>>();
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Provider).Returns(role.Provider);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Expression).Returns(role.Expression);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.ElementType).Returns(role.ElementType);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.GetEnumerator()).Returns(role.GetEnumerator());
            _dbContextMock.Setup(x => x.Roles).Returns(mockSetRole.Object);

            //SUT
            _controller = new RoleAPIController(_dbContextMock.Object);

        }

        [Fact]
        public async Task UserAPIController_Get_ReturnRoleList()
        {
            //Arrange

            //Act
            var result = _controller.Get();

            //Asert
            var responseDTO = Assert.IsType<ResponseDTO>(result);
            Assert.Equal(200, responseDTO.StatusCode);
            Assert.True(responseDTO.IsSuccess);
        }

        [Fact]
        public async Task UserAPIController_Get_NullData_ReturnBadRequest()
        {
            //Arrange
            var db = new Mock<FamsContext>();
            var controller = new RoleAPIController(db.Object);

            //Act
            var result = controller.Get();

            //Asert
            var baseResponse = Assert.IsType<ResponseDTO>(result);
            Assert.Equal(404, baseResponse.StatusCode);
            Assert.False(baseResponse.IsSuccess);
            // Assert.Equal("Cannot get role. Please try again.", baseResponse.Message);

        }

    }
}
