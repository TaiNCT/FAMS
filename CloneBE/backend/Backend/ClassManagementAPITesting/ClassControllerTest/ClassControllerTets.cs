using AutoMapper;
using ClassManagementAPI.Controllers;
using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassManagementAPI.Interface;
using Nest;
using Entities.Models;
using ClassManagementAPI.Dto.FilterDTO;
using Microsoft.AspNetCore.Http;
using ClassManagementAPI.Repositories;
using Entities.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using ClassManagementAPI.Dto.SyllabusDTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPITesting.ClassControllerTest
{
    public class ClassControllerTets
    {
        private object _mockRepository;

        // CREAT CLASS
        [Fact]
        public async Task CreateClass_ValidModel_ReturnsOk()
        {
            // Arrange
            var model = new ClassCreateDto
            {
                CreatedBy = "John Doe",
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedBy = "Jane Smith",
                UpdatedDate = DateTime.Now.AddDays(-3),
                ClassStatus = "Active",
                ClassCode = "ABC123",
                Duration = 60,
                StartDate = new DateOnly(2024, 3, 15),
                EndDate = new DateOnly(2024, 3, 20),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(12, 0),
                ApprovedBy = "Manager",
                ApprovedDate = DateTime.Now.AddDays(-1),
                ReviewBy = "Supervisor",
                ReviewDate = DateTime.Now.AddDays(-2),
                AcceptedAttendee = 25,
                ActualAttendee = 20,
                ClassName = "Introduction to Programming",
                FsuId = "FSU123",
                LocationId = "LC12345",
                AttendeeLevelId = "Beginner",
                TrainingProgramCode = "TPC456",
                PlannedAttendee = 30,
                SlotTime = "Morning",
                TotalDays = 5,
                TotalHours = 15.5
            };
            Class createdClass = null;
            var mockLogger = new Mock<ILogger<ClassController>>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Class>(It.IsAny<ClassCreateDto>())).Returns(new Class());
            var mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(repo => repo.CreateClass(It.IsAny<Class>())).ReturnsAsync(true).Callback<Class>(c => createdClass = c); ;
            var mockElasticClient = new Mock<IElasticClient>();
            var mockLocationRepository = new Mock<ILocationRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockClassUserRepository = new Mock<IClassUserRepository>();

            var mockSyllabusRepository = new Mock<ISyllabusRepository>();

            var controller = new ClassController(
                mockLogger.Object,
                mockElasticClient.Object,
                mockMapper.Object,
                mockClassRepository.Object,
                mockLocationRepository.Object,
                mockUserRepository.Object,
                mockClassUserRepository.Object,
                mockSyllabusRepository.Object);

            // Act
            var result = await controller.CreateClass(model) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            //check call CreateClass
            mockClassRepository.Verify(repo => repo.CreateClass(It.IsAny<Class>()), Times.AtLeastOnce);

        }

        [Fact]
        public async Task CreateClass_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var model = new ClassCreateDto();
            var mockLogger = new Mock<ILogger<ClassController>>();
            var mockMapper = new Mock<IMapper>();
            var mockClassRepository = new Mock<IClassRepository>();
            var mockElasticClient = new Mock<IElasticClient>();
            var mockLocationRepository = new Mock<ILocationRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockClassUserRepository = new Mock<IClassUserRepository>();

            var mockSyllabusRepository = new Mock<ISyllabusRepository>();

            var controller = new ClassController(
                mockLogger.Object,
                mockElasticClient.Object,
                mockMapper.Object,
                mockClassRepository.Object,
                mockLocationRepository.Object,
                mockUserRepository.Object,
                mockClassUserRepository.Object,
                mockSyllabusRepository.Object);

            controller.ModelState.AddModelError("ClassName", "ClassName is required"); // Adding an error to model state

            // Act
            var result = await controller.CreateClass(model) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task CreateClass_WhenSaved_SavesDraft()  // creat class có status là Planing và đc save thành công
        {
            // Arrange
            var model = new ClassCreateDto
            {
                CreatedBy = "John Doe",
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedBy = "Jane Smith",
                UpdatedDate = DateTime.Now.AddDays(-3),
                ClassStatus = "Planning",
                ClassCode = "ABC123",
                Duration = 60,
                StartDate = new DateOnly(2024, 3, 15),
                EndDate = new DateOnly(2024, 3, 20),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(12, 0),
                ApprovedBy = "Manager",
                ApprovedDate = DateTime.Now.AddDays(-1),
                ReviewBy = "Supervisor",
                ReviewDate = DateTime.Now.AddDays(-2),
                AcceptedAttendee = 25,
                ActualAttendee = 20,
                ClassName = "Introduction to Programming",
                FsuId = "FSU123",
                LocationId = "LC12345",
                AttendeeLevelId = "Beginner",
                TrainingProgramCode = "TPC456",
                PlannedAttendee = 30,
                SlotTime = "Morning",
                TotalDays = 5,
                TotalHours = 15.5
            };
            Class createdClass = null;
            var mockLogger = new Mock<ILogger<ClassController>>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Class>(It.IsAny<ClassCreateDto>())).Returns((ClassCreateDto dto) =>
            {
                var classModel = new Class();
                classModel.ClassStatus = dto.ClassStatus;
                return classModel;
            });
            var mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(repo => repo.CreateClass(It.IsAny<Class>()))
                               .ReturnsAsync(true)
                               .Callback<Class>(c => createdClass = c);
            var mockElasticClient = new Mock<IElasticClient>();
            var mockLocationRepository = new Mock<ILocationRepository>();
            var mockUserRepository = new Mock<IUserRepository>(); // Thêm mock cho IUserRepository
            var mockClassUserRepository = new Mock<IClassUserRepository>(); // Thêm mock cho IClassUserRepository
            var mockSyllabusRepository = new Mock<ISyllabusRepository>();

            var controller = new ClassController(
                mockLogger.Object,
                mockElasticClient.Object,
                mockMapper.Object,
                mockClassRepository.Object,
                mockLocationRepository.Object,
                mockUserRepository.Object,
                mockClassUserRepository.Object,
                mockSyllabusRepository.Object);

            // Act
            var result = await controller.CreateClass(model) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(createdClass);
            Assert.Equal("Planning", createdClass.ClassStatus);
        }

        //Filter week
        [Fact]
        public async Task GetClassesListInAWeekByFilter_Returns_OkObjectResult_With_Valid_FilterData()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ClassController>>();
            var classRepositoryMock = new Mock<IClassRepository>();
            var mapperMock = new Mock<IMapper>();

            // Mocking dependencies
            var filterDataList = new PagedResult<WeekResultDto>();
            var mappedResult = new PagedResult<WeekResultDto>();
            var responseDto = new ResponseDto("Operation successful", 200, true, mappedResult);

            classRepositoryMock.Setup(repo => repo.GetClassListInWeekByFilter(It.IsAny<FilterFormatDto>())).ReturnsAsync(filterDataList);
            mapperMock.Setup(mapper => mapper.Map<PagedResult<WeekResultDto>>(filterDataList)).Returns(mappedResult);

            var controller = new ClassController(loggerMock.Object, null, mapperMock.Object, classRepositoryMock.Object, null, null, null, null);

            // Act
            var result = await controller.GetClassesListInAWeekByFilter(new FilterFormatDto());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseDto>(okObjectResult.Value);
            Assert.True(response.Success);
            Assert.Equal("Operation successful", response.Message);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(mappedResult, response.Data);
        }

        [Fact]
        public async Task GetClassesListInAWeekByFilter_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ClassController>>();
            var classRepositoryMock = new Mock<IClassRepository>();
            var mapperMock = new Mock<IMapper>();

            classRepositoryMock.Setup(repo => repo.GetClassListInWeekByFilter(It.IsAny<FilterFormatDto>())).ThrowsAsync(new Exception("Test Exception"));

            var controller = new ClassController(loggerMock.Object, null, mapperMock.Object, classRepositoryMock.Object, null, null, null, null);

            // Act
            var result = await controller.GetClassesListInAWeekByFilter(new FilterFormatDto());

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsType<ResponseDto>(objectResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Error retrieving data from the database", response.Message);
            Assert.Equal(500, response.StatusCode);
            Assert.Equal("Test Exception", response.Data); // Assuming you want to assert the exception message here
        }

        //View class infor
        [Fact]
        public async Task ViweInfoClass_Returns_OkResult_With_InfoClassDto_When_ClassIdIsNotNull()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ClassController>>();
            var classRepositoryMock = new Mock<IClassRepository>();
            var locationRepositoryMock = new Mock<ILocationRepository>();
            var classUserRepositoryMock = new Mock<IClassUserRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var syllabusRepositoryMock = new Mock<ISyllabusRepository>();

            var classId = "CID1";
            var userType = "Student";

            var classNow = new Class { LocationId = "locationId" };
            var location = new Location { Name = "Binh Duong" };
            var listUserId = new List<ClassUser> { new ClassUser { UserId = "UID1" }, new ClassUser { UserId = "UID2" } };
            var users = new List<User> { new User { FullName = "LEO" }, new User { FullName = "FANCY" } };

            classRepositoryMock.Setup(repo => repo.GetClassById(classId)).ReturnsAsync(classNow);
            locationRepositoryMock.Setup(repo => repo.GetLocationById(classNow.LocationId)).ReturnsAsync(location);
            classUserRepositoryMock.Setup(repo => repo.GetClassUsersByClassID(classId, userType)).ReturnsAsync(listUserId);
            userRepositoryMock.Setup(repo => repo.GetAllUserByListUserID(listUserId)).ReturnsAsync(users);

            var controller = new ClassController(
                loggerMock.Object,
                null,
                null,
                classRepositoryMock.Object,
                locationRepositoryMock.Object,
                userRepositoryMock.Object,
                classUserRepositoryMock.Object,
                syllabusRepositoryMock.Object);

            // Act
            var result = await controller.GetInfoClass(classId, userType);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseDto>(okObjectResult.Value);
            var infoClassDto = Assert.IsType<ViewInfoClassDto>(response.Data);
            Assert.True(response.Success);
            Assert.Equal("Operation successful", response.Message);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(location.Name, infoClassDto.Location);
            Assert.Equal(users, infoClassDto.Users);
        }

        [Fact] //BadRequest when id = null
        public async Task ViewInfoClass_Returns_BadRequest_When_ClassIdIsNull()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ClassController>>();
            var classRepositoryMock = new Mock<IClassRepository>();
            var locationRepositoryMock = new Mock<ILocationRepository>();
            var classUserRepositoryMock = new Mock<IClassUserRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var syllabusRepositoryMock = new Mock<ISyllabusRepository>();

            var controller = new ClassController(
                loggerMock.Object,
                null,
                null,
                classRepositoryMock.Object,
                locationRepositoryMock.Object,
                userRepositoryMock.Object,
                classUserRepositoryMock.Object,
                syllabusRepositoryMock.Object);

            // Act
            var result = await controller.GetInfoClass(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ResponseDto>(badRequestResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Operation fail", response.Message);
            Assert.Equal(400, response.StatusCode);
        }

        //View class detail
        [Fact]
        public async Task ViewDetailInfoClass_Returns_OkResult_With_InfoClassDetailDto_When_ClassIdIsNotNull()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ClassController>>();
            var classRepositoryMock = new Mock<IClassRepository>();

            var classId = "CID1";
            var classNow = new Class
            {
                ClassId = classId,
                TotalHours = 30,
                TotalDays = 10,
                StartTime = new TimeOnly(9, 0, 0),
                EndTime = new TimeOnly(18, 0, 0),
                StartDate = new DateOnly(2024, 3, 1),
                EndDate = new DateOnly(2024, 3, 11),
                ClassCode = "ClassCode123",
                ClassStatus = "Active",
                ClassName = "Class ABC",
                ReviewBy = "Reviewer",
                ReviewDate = new DateTime(2024, 3, 12),
                ApprovedBy = "Approver",
                ApprovedDate = new DateTime(2024, 3, 13),
                CreatedBy = "Creator",
                CreatedDate = new DateTime(2024, 2, 29),
                AcceptedAttendee = 20,
                ActualAttendee = 15,
                PlannedAttendee = 25,
                Fsu = new Fsu { Name = "FSU Name", Email = "fsu@example.com" },
                TrainingProgramCodeNavigation = new TrainingProgram
                {
                    UpdatedBy = "Trainer",
                    UpdatedDate = new DateTime(2024, 2, 28),
                    Days = 10,
                    Hours = 30,
                    Name = "Training Program ABC"
                },
                Location = new Location { Name = "Location XYZ" },
                ClassUsers = new List<ClassUser>
                {
                    new ClassUser
                    {
                        User = new User
                        {
                            Id = 2,
                            FullName = "User One",
                            Email = "user.one@example.com",
                            Phone = "123456789",
                            RoleId = "RoleID1",
                            Role = new Role { RoleName = "Role One" }
                        }
                    },
                    new ClassUser
                    {
                        User = new User
                        {
                            Id = 3,
                            FullName = "User Two",
                            Email = "user.two@example.com",
                            Phone = "987654321",
                            RoleId = "RoleID2",
                            Role = new Role { RoleName = "Role Two" }
                        }
                    }
                }
            };

            classRepositoryMock.Setup(repo => repo.GetClassById(classId)).ReturnsAsync(classNow);

            var controller = new ClassController(
                loggerMock.Object,
                null,
                null,
                classRepositoryMock.Object,
                null,
                null,
                null,
                null
            );

            // Act
            var result = await controller.GetInfoClass(classId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseDto>(okObjectResult.Value);
            var infoClassDetailDto = Assert.IsType<ViewInfoClassDetailDto>(response.Data);
            Assert.True(response.Success);
            Assert.Equal("Operation successful", response.Message);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(classNow.TotalHours, infoClassDetailDto.TotalHours);
        }

        //Get list by DateRange
        [Fact]
        public async Task GetListClassesByDateRange_Returns_OkResult_With_Correct_ResponseDto()
        {
            // Arrange
            var startDate = new DateOnly(2024, 3, 1);
            var endDate = new DateOnly(2024, 3, 31);

            var mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(repo => repo.GetListOfClassesByDateRange(startDate, endDate))
                .ReturnsAsync(new PagedResult<WeekResultDto>
                {
                    TotalCount = 1,
                    Items = new System.Collections.Generic.List<WeekResultDto> {
                        new WeekResultDto {
                            DayOfWeek = "Monday",
                            ClassOfWeek = new System.Collections.Generic.List<Class> {
                                new Class { Id = 1, ClassName = "Sample Class" }
                            }
                        }
                    }
                });

            var logger = Mock.Of<ILogger<ClassController>>();
            var mockMapper = Mock.Of<IMapper>();
            var mockLocationRepository = Mock.Of<ILocationRepository>();
            var mockUserRepository = Mock.Of<IUserRepository>();
            var mockClassUserRepository = Mock.Of<IClassUserRepository>();
            var mockSyllabusRepository = Mock.Of<ISyllabusRepository>();

            var controller = new ClassController(logger, null, mockMapper, mockClassRepository.Object,
                                                 mockLocationRepository, mockUserRepository,
                                                 mockClassUserRepository, mockSyllabusRepository);

            // Act
            var result = await controller.GetListClassesByDateRange(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDto = Assert.IsType<ResponseDto>(okResult.Value);

            Assert.True(responseDto.Success);
            Assert.Equal("Get List of Class Successfully", responseDto.Message);
            Assert.Equal(200, responseDto.StatusCode);
            Assert.NotNull(responseDto.Data);

            var pagedResult = Assert.IsType<PagedResult<WeekResultDto>>(responseDto.Data);
            Assert.Equal(1, pagedResult.TotalCount);
            Assert.Single(pagedResult.Items);
            Assert.Equal("Monday", pagedResult.Items[0].DayOfWeek);
            Assert.Single(pagedResult.Items[0].ClassOfWeek);
            Assert.Equal(1, pagedResult.Items[0].ClassOfWeek[0].Id);
            Assert.Equal("Sample Class", pagedResult.Items[0].ClassOfWeek[0].ClassName);
        }

        [Fact]
        public async Task GetListClassesByDateRange_Returns_NotFound_When_No_Classes_Are_Returned()
        {
            // Arrange
            var startDate = new DateOnly(2024, 3, 1);
            var endDate = new DateOnly(2024, 3, 31);

            var mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(repo => repo.GetListOfClassesByDateRange(startDate, endDate))
                .ReturnsAsync(new PagedResult<WeekResultDto> { TotalCount = 0, Items = new List<WeekResultDto>() });

            var logger = Mock.Of<ILogger<ClassController>>();
            var mockMapper = Mock.Of<IMapper>();
            var mockLocationRepository = Mock.Of<ILocationRepository>();
            var mockUserRepository = Mock.Of<IUserRepository>();
            var mockClassUserRepository = Mock.Of<IClassUserRepository>();
            var mockSyllabusRepository = Mock.Of<ISyllabusRepository>();

            var controller = new ClassController(logger, null, mockMapper, mockClassRepository.Object,
                                                 mockLocationRepository, mockUserRepository,
                                                 mockClassUserRepository, mockSyllabusRepository);

            // Act
            var result = await controller.GetListClassesByDateRange(startDate, endDate);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        // Get Student by Attendee
        [Fact]
        public async Task GetByAttendee_Returns_OkResult_With_Attendee_Data()
        {
            // Arrange
            string attendeeId = "attendeeId";
            var mockClassRepository = new Mock<IClassRepository>();
            var logger = Mock.Of<ILogger<ClassController>>();

            // Thiết lập giả lập dữ liệu trả về từ repository
            var expectedClassList = new List<Class> { new Class { Id = 1, ClassName = "NET 01" } };
            mockClassRepository.Setup(repo => repo.GetClassByAttendeeId(attendeeId))
                .ReturnsAsync(expectedClassList);

            var controller = new ClassController(logger, null, null, mockClassRepository.Object, null, null, null, null);

            // Act
            var result = await controller.GetByAttendee(attendeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsType<List<Class>>(okResult.Value);

            Assert.Equal(expectedClassList, responseData);
        }

        [Fact]
        public async Task GetByAttendee_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            string attendeeId = "attendeeId";
            var mockClassRepository = new Mock<IClassRepository>();
            var logger = Mock.Of<ILogger<ClassController>>();

            mockClassRepository.Setup(repo => repo.GetClassByAttendeeId(attendeeId))
                .ThrowsAsync(new Exception("Test exception"));

            var controller = new ClassController(logger, null, null, mockClassRepository.Object, null, null, null, null);

            // Act
            var result = await controller.GetByAttendee(attendeeId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        //Filter by time
        [Fact]
        public async Task GetByTime_Returns_Ok_Result()
        {
            // Arrange
            var filterTime = new FilterTime { FromDate = new DateOnly(2024, 3, 1), ToDate = new DateOnly(2024, 3, 31) };
            var mockRepository = new Mock<IClassRepository>();
            mockRepository.Setup(repo => repo.GetClassByTime(filterTime))
                          .ReturnsAsync(new List<Class>());
            var mockLogger = Mock.Of<ILogger<ClassController>>();
            var mockElasticClient = Mock.Of<IElasticClient>();
            var mockMapper = Mock.Of<IMapper>();
            var mockLocationRepository = Mock.Of<ILocationRepository>();
            var mockUserRepository = Mock.Of<IUserRepository>();
            var mockClassUserRepository = Mock.Of<IClassUserRepository>();
            var mockSyllabusRepository = Mock.Of<ISyllabusRepository>();

            var controller = new ClassController(
                mockLogger,
                mockElasticClient,
                mockMapper,
                mockRepository.Object,
                mockLocationRepository,
                mockUserRepository,
                mockClassUserRepository,
                mockSyllabusRepository
            );

            // Act
            var result = await controller.GetByTime(filterTime);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var classes = Assert.IsAssignableFrom<IEnumerable<Class>>(okResult.Value);
            Assert.Empty(classes);
        }

        //Get List Syllabus
        [Fact]
        public async Task GetListSyllabus_Returns_OkObjectResult_With_Valid_TrainingCode()
        {
            // Arrange
            var mockRepository = new Mock<ISyllabusRepository>();
            var controller = new ClassController(Mock.Of<ILogger<ClassController>>(), null, Mock.Of<IMapper>(), Mock.Of<IClassRepository>(), Mock.Of<ILocationRepository>(), Mock.Of<IUserRepository>(), Mock.Of<IClassUserRepository>(), mockRepository.Object);
            string validTrainingCode = "validTrainingCode";
            var expectedSyllabusList = new PagedResult<Syllabus>();

            mockRepository.Setup(repo => repo.GetListSyllabus(validTrainingCode))
                          .ReturnsAsync(expectedSyllabusList);

            // Act
            var result = await controller.GetListSyllabus(validTrainingCode);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var responseDto = Assert.IsType<ResponseDto>(okObjectResult.Value);
            Assert.Equal("Successfull", responseDto.Message);
            Assert.True(responseDto.Success);
            Assert.Equal(expectedSyllabusList, responseDto.Data);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task GetListSyllabus_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            string invalidTrainingCode = "invalidTrainingCode";
            var mockRepository = new Mock<ISyllabusRepository>();
            var controller = new ClassController(Mock.Of<ILogger<ClassController>>(), null, Mock.Of<IMapper>(), Mock.Of<IClassRepository>(), Mock.Of<ILocationRepository>(), Mock.Of<IUserRepository>(), Mock.Of<IClassUserRepository>(), mockRepository.Object);

            mockRepository.Setup(repo => repo.GetListSyllabus(invalidTrainingCode))
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.GetListSyllabus(invalidTrainingCode);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var responseDto = Assert.IsType<ResponseDto>(objectResult.Value);
            Assert.Equal("Error retrieving data from the database", responseDto.Message);
            Assert.False(responseDto.Success);
            Assert.Equal("Test exception", responseDto.Data);
        }

        [Fact]
        public async Task AddProgramSyllabus_ValidData_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<ISyllabusRepository>();
            var mockLogger = new Mock<ILogger<ClassController>>();
            var mockMapper = new Mock<IMapper>();
            var controller = new ClassController(mockLogger.Object, null, mockMapper.Object, null, null, null, null, mockRepository.Object); // Truyền mock của IMapper vào constructor của ClassController
            var syllabusDTO = new InsertProgramSyllabusDTO
            {
                SyllabusId = "1",
                TrainingProgramCode = "ABC"
            };

            // Act
            var result = await controller.AddProgramSyllabus(syllabusDTO) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(201, result.StatusCode);
            Assert.True((bool)result.Value.GetType().GetProperty("Success").GetValue(result.Value));
        }

    }
}
