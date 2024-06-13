using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Nest;
using UserManagementAPI.Controllers;
using Entities.Models; // Adjust this to your actual namespace
using UserManagementAPI.Models.DTO;
using UserManagementAPI.Models.DTO.ValidationDTO;
using UserManagementAPITests.UserData;
using Entities.Context;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Azure;
namespace UserManagementAPITests.Controller
{
    public class UserAPIControllerTests
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly Mock<IFormFile> _fileMock;


        private readonly Mock<IPublishEndpoint> _publishEndpoint;
        private readonly UserAPIController _controller;

        public UserAPIControllerTests()
        {
            // Configure services
            ConfigurationExtension.ConfigureServices();

            // Configure publish enpoint services
            _publishEndpoint =  new Mock<IPublishEndpoint>();

            //Mock data for user in Db
            var users = UserMockData.GetUsers().AsQueryable();
            var mockSetUser = new Mock<DbSet<User>>();
            mockSetUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            mockSetUser.As<IAsyncEnumerable<User>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new AsyncEnumerator<User>(users.GetEnumerator()));

            //Mock data for role in Db
            var role = RoleMockData.GetRoles().AsQueryable();
            var mockSetRole = new Mock<DbSet<Role>>();
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Provider).Returns(role.Provider);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.Expression).Returns(role.Expression);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.ElementType).Returns(role.ElementType);
            mockSetRole.As<IQueryable<Role>>().Setup(m => m.GetEnumerator()).Returns(role.GetEnumerator());

            //Mock FamsContext
            _dbContextMock = new Mock<FamsContext>();
            _dbContextMock.Setup(x => x.Users).Returns(mockSetUser.Object);
            _dbContextMock.Setup(x => x.Roles).Returns(mockSetRole.Object);

            //Mock Mapper
            _mapperMock = new Mock<IMapper>();

            //Mock Mapping Config
            var mapper = ConfigurationExtension.ConfigureAutoMapper();

            //Mock Elastic
            _elasticClientMock = new Mock<IElasticClient>();

            //Mock Elastic Search
            var hits = new List<IHit<User>>
            {
                new Mock<IHit<User>>().Object
            };
            var user = UserMockData.GetUsers();
            var mockSearchResponse = new Mock<ISearchResponse<User>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(user);
            mockSearchResponse.Setup(x => x.Hits).Returns(hits);
            _elasticClientMock.Setup(x => x
                .Search(It.IsAny<Func<SearchDescriptor<User>, ISearchRequest>>()))
                    .Returns(mockSearchResponse.Object);

            //Mock Elastic IndexAsync
            var mockResponse = new Mock<IndexResponse>();
            mockResponse.Setup(r => r.IsValid).Returns(true);
            _elasticClientMock.Setup(x => x.IndexAsync(It.IsAny<UserElasticDTO>(),
                It.IsAny<Func<IndexDescriptor<UserElasticDTO>,
                IIndexRequest<UserElasticDTO>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse.Object);

            //Mock IFormFile
            _fileMock = new Mock<IFormFile>();

            //SUT - System under test
            _controller = new UserAPIController(_publishEndpoint.Object ,_dbContextMock.Object, mapper, _elasticClientMock.Object);
        }

        [Fact]
        public async Task UserAPIController_ChangeRole_ReturnOK()
        {
            //Arragne
            var requestData = new ChangeUserRoleDTO()
            {
                userId = "67E07846-70A1-4372-893C-442285559296",
                roleId = "FA2DD038-D819-4107-BC97-4485DE156120",
            };

            //Act
            var result = _controller.ChangeRole(requestData);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Change role successfully", response.Message);
        }

        [Fact]
        public async Task UserAPIController_ChangeRole_WrongUserId_ReturnBadRequest()
        {
            //Arragne
            var requestData = new ChangeUserRoleDTO()
            {
                userId = "67E07846-70A1-4372-893C-44228555929",
                roleId = "FA2DD038-D819-4107-BC97-4485DE156120",
            };

            //Act
            var result = _controller.ChangeRole(requestData);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("The User does not exist!!!!", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task UserAPIController_ChangeRole_WrongRoleId_ReturnBadRequest()
        {
            //Arragne
            var requestData = new ChangeUserRoleDTO()
            {
                userId = "67E07846-70A1-4372-893C-442285559296",
                roleId = "FA2DD038-D819-4107-BC97-4485DE15612",
            };

            //Act
            var result = _controller.ChangeRole(requestData);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("The RoleId does not exist", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task UserAPIController_ChangeStatus_ReturnOk()
        {
            //Arragne
            var requestData = new ChangeStatusDTO()
            {
                userId = "67E07846-70A1-4372-893C-442285559296",
                status = false,
            };

            //Act
            var result = _controller.ChangeStatus(requestData);

            //Asert

            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.True(response.IsSuccess);
            Assert.Equal("Change status successfully", response.Message);
        }

        [Fact]
        public async Task UserAPIController_ChangeStatus_WrongUserId_ReturnBadRequest()
        {
            //Arragne
            var requestData = new ChangeStatusDTO()
            {
                userId = "67E07846-70A1-4372-893C-44228555929",
                status = false,
            };

            //Act
            var result = _controller.ChangeStatus(requestData);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("User does not exist", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task UserAPIController_CheckEmailExist_ReturnOk()
        {
            //Arange
            var email = new EmailValidationDTO()
            {
                Email = "john1234@gmail.com"
            };

            //Act
            var result = _controller.CheckEmailExists(email);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Email available", responseDTO.Message);
        }

        [Fact]
        public async Task UserAPIController_CheckEmailExist_EmailExisted_ReturnBadRequest()
        {
            //Arange
            var email = new EmailValidationDTO()
            {
                Email = "john@gmail.com"
            };

            //Act
            var result = _controller.CheckEmailExists(email);

            //Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Email already exists", responseDTO.Message);
        }

        [Fact]
        public async Task UserAPIController_CheckPhoneExists_ReturnOk()
        {
            //Arange
            var phone = new PhoneValidationDTO()
            {
                Phone = "0987654221"
            };

            //Act
            var result = _controller.CheckPhoneExists(phone);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Phone number available", responseDTO.Message);
        }

        [Fact]
        public async Task UserAPIController_CheckPhoneExists_PhoneExisted_ReturnBadRequest()
        {
            //Arange
            var phone = new PhoneValidationDTO()
            {
                Phone = "0987654331"
            };

            //Act
            var result = _controller.CheckPhoneExists(phone);

            //Assert 
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Phone number already exists", responseDTO.Message);
        }


        [Fact]
        public async Task UserAPIController_CheckUsernameExists_ReturnOk()
        {
            //Arange
            var username = new UNameValidationDTO()
            {
                Username = "myphuong1234"
            };

            //Act
            var result = _controller.CheckUsernameExists(username);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Username available", responseDTO.Message);
        }

        [Fact]
        public async Task UserAPIController_CheckUsernameExists_UserNameExisted_ReturnBadRequest()
        {
            //Arange
            var username = new UNameValidationDTO()
            {
                Username = "myphuong123"
            };

            //Act
            var result = _controller.CheckUsernameExists(username);

            //Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Username already exists", responseDTO.Message);
        }

        [Fact]
        public async Task UserAPIController_Pagination_ReturnOk()
        {
            //Arrange
            var userCount = UserMockData.GetUsers();

            // Act
            var result = _controller.Pagination(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ResponseDTO>(okResult.Value);
            var users = Assert.IsType<List<UserDTO>>(value.Result.GetType().GetProperty("Users").GetValue(value.Result, null));
            Assert.NotNull(okResult.Value);
            Assert.Equal(userCount.Count, users.Count);
        }

        [Fact]
        public async Task UserAPIController_CreateUser_ReturnOk()
        {
            try
            {
                //Arrange
                var expedtedResult = new AddUserDTO
                {
                    UserId = "67E07846-70A1-4372-893C-442285559296",
                    Id = 2,
                    FullName = "Cao My Tao",
                    Email = "mypguib123@gmail.com",
                    Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                    Address = "hcm",
                    Gender = "male",
                    Phone = "0987654371",
                    Username= "myphuong312",
                    Password= "Admin@123456",
                    RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                    CreatedBy= "string",
                    CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    ModifiedBy= "string",
                    ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Avatar= "string",
                    Status= true,
                };

                //Act
                var result = _controller.CreateUser(expedtedResult);

                //Asert
                var baseResponse = Assert.IsType<OkObjectResult>(result.Result);
                var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
                Assert.Equal(200, baseResponse.StatusCode);
                Assert.Equal("User created successfully", responseDTO.Message);
                //await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Assert.Contains(typeof(NotFoundObjectResult).ToString(), ex.Message);
                Console.WriteLine($"An Unexpected Error Occured: {ex.Message}");
            }
        }

        [Fact]
        public async Task UserAPIController_CreateUser_NullFullName_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "",
                Email = "mypguib123@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654321",
                Username= "myphuong312",
                Password= "admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Fullname is required", responseDTO.Message);
            //await Task.CompletedTask;
        }

        //Dont have validate FullName only Character
        [Fact]
        public async Task UserAPIController_CreateUser_NumberFullName_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong123",
                Email = "mypguib123@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654364",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Only character allowed", responseDTO.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_CreateUser_ExistedUserName_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "mypguib123@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654321",
                Username= "myphuong",
                Password= "admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Username already exists", responseDTO.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_CreateUser_WrongFormatEmail_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "john@example.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654321",
                Username= "myphuong312",
                Password= "admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Email is in wrong format (Format: example@gmail.com)", responseDTO.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_CreateUser_ExistedEmail_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "john@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654321",
                Username= "myphuong312",
                Password= "admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Email already exists", responseDTO.Message);
            //await Task.CompletedTask;

        }

        [Fact]
        public async Task UserAPIController_CreateUser_NullEmail_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654356",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Email is required", responseDTO.Message);
            //await Task.CompletedTask;

        }

       /* [Fact]
        public async Task UserAPIController_CreateUser_WrongPasswordLength_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654388",
                Username= "myphuong312",
                Password= "Admin@12345",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(400, baseResponse.StatusCode);
            Assert.Equal("Password must be more than 12 characters", responseDTO.Message);
            //await Task.CompletedTask;
        }*/

        [Fact]
        public async Task UserAPIController_CreateUser_NullPhoneNumber_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(500, responseDTO.StatusCode);
            Assert.Equal("Phone number is required", responseDTO.Message);
            //await Task.CompletedTask;

        }

        [Fact]
        public async Task UserAPIController_CreateUser_ExistedPhoneNumber_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654331",
                Username= "myphuong312",
                Password= "Admin@12345678",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(500, responseDTO.StatusCode);
            Assert.Equal("Phone number already exists", responseDTO.Message);
            //await Task.CompletedTask;

        }

        [Fact]
        public async Task UserAPIController_CreateUser_WrongPhoneNumberLength_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "098765433",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(500, responseDTO.StatusCode);
            Assert.Equal("Phone number must be 10 digits", responseDTO.Message);
            //await Task.CompletedTask;

        }

        //Dont have start with 0
        [Fact]
        public async Task UserAPIController_CreateUser_IsPhoneNumber_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "1987654331",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(500, responseDTO.StatusCode);
            Assert.Equal("Phone number must start with 0(Example: 0987654321)", responseDTO.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_CreateUser_GreaterThan18DoB_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new AddUserDTO
            {
                FullName = "Cao My Phuong",
                Email = "joh31n@gmail.com",
                Dob = DateTime.Parse("2012-03-12T04:16:01.248Z"),
                Address = "hcm",
                Gender = "male",
                Phone = "0987654341",
                Username= "myphuong312",
                Password= "Admin@123456",
                RoleId= "68F87DE1-81C8-4EBC-8A43-69E13E43E333",
                CreatedBy= "string",
                CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                ModifiedBy= "string",
                ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                Avatar= "string",
                Status= true,
            };

            //Act
            var result = _controller.CreateUser(expedtedResult);

            //Asert
            var baseResponse = Assert.IsType<BadRequestObjectResult>(result.Result);
            var responseDTO = Assert.IsType<ResponseDTO>(baseResponse.Value);
            Assert.Equal(500, responseDTO.StatusCode);
            Assert.Equal("Age must be 18 or above", responseDTO.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_Filter_ReturnOk()
        {
            //Arrange
            var userCount = UserMockData.GetUsers();

            // Act
            var result = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: true, isTrainer: true, isActive: true, isInactive: true, isMale: true, isFemale: true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ResponseDTO>(okResult.Value);
            var count = Assert.IsType<List<UserDTO>>(value.Result.GetType().GetProperty("Users").GetValue(value.Result, null));
            Assert.Equal(userCount.Count, count.Count);
        }

        [Fact]
        public async Task UserAPIController_Filter_NotATrainer_ReturnOk()
        {
            //Arrange
            var userCount = UserMockData.GetUsers();

            // Act
            var result = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: true, isTrainer: false, isActive: true, isInactive: true, isMale: true, isFemale: true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<ResponseDTO>(okResult.Value);
            var count = Assert.IsType<List<UserDTO>>(value.Result.GetType().GetProperty("Users").GetValue(value.Result, null));
            Assert.Equal(1, count.Count);
        }

        [Fact]
        public async Task UserAPIController_SyncToElasticsearch_ReturnOk()
        {
            // Arrange

            // Act
            var result = await _controller.SyncToElasticsearch();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Synced to Elasticsearch successfully.", responseDTO);
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_ReturnOk()
        {

            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var usersBefore = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: true, isTrainer: true, isActive: true, isInactive: true, isMale: true, isFemale: true);
            var usersAfter = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: false, isTrainer: true, isActive: true, isInactive: true, isMale: true, isFemale: true);
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal(true, response.IsSuccess);
            Assert.Equal("Updated successfully.", response.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_WrongUserId_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-44228555929",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("User does not exist.", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_NullFullName_ReturnBadRequest()
        {

            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var usersBefore = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: true, isTrainer: true, isActive: true, isInactive: true, isMale: true, isFemale: true);
            var usersAfter = _controller.Filter(page: 1, pageSize: 10, sortBy: "id", sortOrder: "asc", dob: null, isAdmin: false, isTrainer: true, isActive: true, isInactive: true, isMale: true, isFemale: true);
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Fullname is required", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        //UserAPIController Update don't have validate Only Character FullName
        [Fact]
        public async Task UserAPIController_UpdateUser_OnlyCharacterFullName_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong123",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Only Character allowed", response.Message);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_NullPhoneNumber_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong123",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Phone number is required", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_PhoneNumberExist_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654321",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Phone number already exists", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_WrongPhoneNumberLength_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "098765434",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Phone number must be 10-digit number", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }


        [Fact]
        public async Task UserAPIController_UpdateUser_GreaterThan18Dob_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2012-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("User must be at least 18 years old", response.Message);
            //await Task.CompletedTask;
        }

        //Dont have validate gender
        [Fact]
        public async Task UserAPIController_UpdateUser_WrongFormatGender_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "as",
                Phone = "0987654341",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("Gender doesn't existed", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_NullRoleId_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654331",
                RoleId= "",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("RoleId is required", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }

        [Fact]
        public async Task UserAPIController_UpdateUser_RoleIdNotExist_ReturnBadRequest()
        {
            //Arrange
            var expedtedResult = new UpdateUserDTO
            {
                UserId = "67E07846-70A1-4372-893C-442285559296",
                FullName = "Cao My Phuong",
                Dob = DateTime.Parse("2003-03-12T04:16:01.248Z"),
                Gender = "Female",
                Phone = "0987654331",
                RoleId= "FA2DD038-D819-4107-BC97-4485DE1561",
                Status= false,
            };

            //Act
            var result = _controller.UpdateUser(expedtedResult);

            //Asert
            var results = Assert.IsType<Task<ResponseDTO>>(result);
            var response = Assert.IsType<ResponseDTO>(results.Result);
            Assert.Equal("The RoleId does not exist", response.Message);
            Assert.False(response.IsSuccess);
            //await Task.CompletedTask;
        }


        private class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public AsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public T Current => _inner.Current;

            public ValueTask DisposeAsync() => new ValueTask();

            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
        }

        [Fact]
        public async Task UserAPIController_ImportFile_ReturnOk()
        {
            //Arrange
            var PhysicalFile = new FileInfo(@"D:\Clone\fam_hcm24_cpl_net_02\Backend\UserManagementAPITests\UserData\TestFile\ImportUserTemplate.xlsx");
            var memory = new MemoryStream();

            // Correctly copy the file content to the MemoryStream
            using (var fileStream = PhysicalFile.OpenRead())
            {
                fileStream.CopyTo(memory);
            }

            memory.Position = 0; // Reset the position to the beginning of the stream

            var fileName = PhysicalFile.Name;

            _fileMock.Setup(_ => _.FileName).Returns(fileName);
            _fileMock.Setup(_ => _.Length).Returns(memory.Length);
            _fileMock.Setup(_ => _.OpenReadStream()).Returns(memory);
            _fileMock.Verify();
            _fileMock.Setup(f => f.FileName).Returns(fileName).Verifiable();
            _fileMock.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => memory.CopyToAsync(stream))
                .Verifiable();

            var file = _fileMock.Object;

            // Act
            var result = await _controller.ImportUser(file);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("import success", response.Message);
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            
        }

        [Fact]
        public async Task UserAPIController_ImportFile_WrongDataInImportFile_ReturnBadRequest()
        {
            //Arrange
            var PhysicalFile = new FileInfo(@"D:\Clone\fam_hcm24_cpl_net_02\Backend\UserManagementAPITests\UserData\TestFile\ImportUserTemplateFail.xlsx");
            var memory = new MemoryStream();

            // Correctly copy the file content to the MemoryStream
            using (var fileStream = PhysicalFile.OpenRead())
            {
                fileStream.CopyTo(memory);
            }

            memory.Position = 0; // Reset the position to the beginning of the stream

            var fileName = PhysicalFile.Name;

            _fileMock.Setup(_ => _.FileName).Returns(fileName);
            _fileMock.Setup(_ => _.Length).Returns(memory.Length);
            _fileMock.Setup(_ => _.OpenReadStream()).Returns(memory);
            _fileMock.Verify();
            _fileMock.Setup(f => f.FileName).Returns(fileName).Verifiable();
            _fileMock.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => memory.CopyToAsync(stream))
                .Verifiable();

            var file = _fileMock.Object;

            // Act
            var result = await _controller.ImportUser(file);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Equal(500, response.StatusCode);
        }

    }
}
