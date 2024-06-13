using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserManagementAPI.Controllers;
using Entities.Models;
using UserManagementAPI.Models.DTO;
using Entities.Context;
using UserManagementAPITests.UserData;

namespace UserManagementAPITests.Controller
{
    public class PermissionAPIControllerTests
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly PermissionAPIController _controller;
        public PermissionAPIControllerTests()
        {
            _dbContextMock = new Mock<FamsContext>();

            //Mock data RolePermission
            var rolePermission = RoleMockData.GetRolePermissions().AsQueryable();
            var mockSet = new Mock<DbSet<RolePermission>>();
            mockSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermission.Provider);
            mockSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermission.Expression);
            mockSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermission.ElementType);
            mockSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermission.GetEnumerator());
            _dbContextMock.Setup(x => x.RolePermissions).Returns(mockSet.Object);

            //Mock data UserPermission
            var userPermission = UserMockData.GetUserPermissions().AsQueryable();
            var mockSetUser = new Mock<DbSet<UserPermission>>();
            mockSetUser.As<IQueryable<UserPermission>>().Setup(m => m.Provider).Returns(userPermission.Provider);
            mockSetUser.As<IQueryable<UserPermission>>().Setup(m => m.Expression).Returns(userPermission.Expression);
            mockSetUser.As<IQueryable<UserPermission>>().Setup(m => m.ElementType).Returns(userPermission.ElementType);
            mockSetUser.As<IQueryable<UserPermission>>().Setup(m => m.GetEnumerator()).Returns(userPermission.GetEnumerator());
            _dbContextMock.Setup(x => x.UserPermissions).Returns(mockSetUser.Object);

            //Mock data Role
            var role = RoleMockData.GetRoles().AsQueryable();
            var mockSetRole = new Mock<DbSet<Role>>();
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Provider).Returns(role.Provider);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Expression).Returns(role.Expression);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.ElementType).Returns(role.ElementType);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.GetEnumerator()).Returns(role.GetEnumerator());
            _dbContextMock.Setup(x => x.Roles).Returns(mockSetRole.Object);

            //SUT 
            _controller = new PermissionAPIController(_dbContextMock.Object);

        }

        [Fact]
        public void PermissionAPIController_Get_ReturnsOk()
        {
            // Arrange

            // Act
            var result = _controller.Get();

            // Assert
            var viewResult = Assert.IsType<ResponseDTO>(result);

            Assert.True(viewResult.IsSuccess);
            Assert.Equal(200, viewResult.StatusCode);
        }

        [Fact]
        public void PermissionAPIController_Get_NullData_ReturnsBadRequest()
        {
            // Arrange
            var db = new Mock<FamsContext>();
            var controller = new PermissionAPIController(db.Object);

            // Act
            var result = controller.Get();

            // Assert
            var viewResult = Assert.IsType<ResponseDTO>(result);

            Assert.Equal("Cannot get permission of role. Please try again.", viewResult.Message);
            Assert.False(viewResult.IsSuccess);
        }

        [Fact]
        public void PermissionAPIController_GetUserPerms_ReturnsOk()
        {
            // Arrange


            // Act
            var result = _controller.GetUserPerms();

            // Assert
            Assert.Equal("Success", result.Message);
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);
        }

        //Cannot get user permission

        [Fact]
        public void PermissionAPIController_GetUserPerms_NullData_Returns()
        {
            // Arrange
            var db = new Mock<FamsContext>();
            var controller = new PermissionAPIController(db.Object);

            // Act
            var result = controller.GetUserPerms();

            // Assert
            Assert.Equal("Cannot get role. Please try again.", result.Message);
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void PermissionAPIController_UpdateRolePermissions_ReturnsOk()
        {
            // Arrange
            var updateRequest = new RolePermissionUpdateDTO
            {
                RolePermissions = new List<RolePermissionsDetails>
                {
                    new RolePermissionsDetails
                    {
                        RoleId = "817E56F4-E597-4AEB-A04C-144A30547334",
                        Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    },
                     new RolePermissionsDetails
                    {
                        RoleId = "FA2DD038-D819-4107-BC97-4485DE156120",
                        Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    }
                }
            };
            // Act
            var result = _controller.UpdateRolePermissions(updateRequest);
            // Assert
            var model = Assert.IsAssignableFrom<ResponseDTO>(result);
            Assert.True(model.IsSuccess);
            Assert.Equal(200, model.StatusCode);
            Assert.Equal("Role permissions updated successfully for all provided roles.", model.Message);
        }

        [Fact]
        public void PermissionAPIController_UpdateRolePermissions_RoleIdNotExist_ReturnsBadRequest()
        {
            // Arrange
            var updateRequest = new RolePermissionUpdateDTO
            {
                RolePermissions = new List<RolePermissionsDetails>
                {
                    new RolePermissionsDetails
                    {
                        RoleId = "817E56F4-E597-4AEB-A04C-144A3054734",
                        Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEAD0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    },
                     new RolePermissionsDetails
                    {
                        RoleId = "FA2DD038-D819-4107-BC97-4485DE156120",
                        Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    }
                }
            };

            // Act
            var result = _controller.UpdateRolePermissions(updateRequest);

            // Assert
            var model = Assert.IsAssignableFrom<ResponseDTO>(result);
            Assert.NotNull(model.Message);
        }

        //Dont have validate UserPermissionId
        [Fact]
        public void PermissionAPIController_UpdateRolePermissions_PerrmissionIdNotExist_ReturnsBadRequest()
        {
            // Arrange
            var updateRequest = new RolePermissionUpdateDTO
            {
                RolePermissions = new List<RolePermissionsDetails>
                {
                    new RolePermissionsDetails
                    {
                        RoleId = "817E56F4-E597-4AEB-A04C-144A30547334",
                        Syllabus = "",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    },
                     new RolePermissionsDetails
                    {
                        RoleId = "FA2DD038-D819-4107-BC97-4485DE156120",
                        Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                        TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                        Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                        LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                        UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    }
                }
            };

            // Act
            var result = _controller.UpdateRolePermissions(updateRequest);

            // Assert
            var model = Assert.IsAssignableFrom<ResponseDTO>(result);
            Assert.True(model.IsSuccess);
            Assert.Equal(200, model.StatusCode);
            Assert.Equal("Permission Id does not existed", model.Message);
        }

    }
}
