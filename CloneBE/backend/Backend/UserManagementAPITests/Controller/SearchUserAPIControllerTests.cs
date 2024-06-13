using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nest;
using UserManagementAPI.Controllers;
using Entities.Models;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPITests.Controller
{
    public class SearchUserAPIControllerTests
    {
        //Dont have ResponseDTO
        [Fact]
        public async Task Search_ReturnsExpectedResponse()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<SearchUserAPIController>>();
            var mockClient = new Mock<IElasticClient>();

            // Mock the SearchAsync method
            var mockSearchResponse = new Mock<ISearchResponse<User>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(new List<User> {
                new User
                {
                    UserId = "67E07846-70A1-4372-893C-442285559296",
                    Id = 99,
                    FullName = "John Doe",
                    Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                    Address = "hcm",
                    Gender = "male",
                    Phone = "0987654331",
                    Username= "myphuong123",
                    Password= "admin@123456",
                    RoleId= "817E56F4-E597-4AEB-A04C-144A30547334",
                    CreatedBy= "string",
                    CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    ModifiedBy= "string",
                    ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Avatar= "string",
                    Status= true,
                    Email = "john@gmail.com",
                    Role = new Role { RoleName = "Admin" ,RoleId= "817E56F4-E597-4AEB-A04C-144A30547334"}
                }
            });
            mockSearchResponse.Setup(x => x.Total).Returns(1);

            mockClient.Setup(x => x.SearchAsync<User>(
                It.IsAny<Func<SearchDescriptor<User>, ISearchRequest>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            var controller = new SearchUserAPIController(mockLogger.Object, mockClient.Object);

            // Act
            var result = await controller.Search("John", 1, 10, "id", "asc");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ResponseDTO>(okResult.Value);
            var users = Assert.IsType<List<User>>(value.Result.GetType().GetProperty("users").GetValue(value.Result, null));
            Assert.Equal(1, users.Count);
        }

    }
}


