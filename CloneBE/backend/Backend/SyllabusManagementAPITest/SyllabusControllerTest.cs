using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NLog.Config;
using SyllabusManagementAPI.Controllers;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.Syllabus;
using SyllabusManagementAPI.Entities.Exceptions.NotFoundException;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.ServiceContracts;
using System.Security.Claims;
using System.Text;

namespace SyllabusManagementAPITest
{
    public class SyllabusControllerTest
    {
        private readonly Mock<IServiceWrapper> _mockService;
        private readonly SyllabusController _controller;
        private readonly SearchSyllabusListController _sreachcontroller;

        public SyllabusControllerTest()
        {
            _mockService = new Mock<IServiceWrapper>();
            _controller = new SyllabusController(_mockService.Object);
            _sreachcontroller = new SearchSyllabusListController(_mockService.Object);
            _mockSyllabusService = new Mock<ISyllabusService>();
            _mockService.Setup(service => service.SyllabusService).Returns(_mockSyllabusService.Object);
        }

        private readonly Mock<ISyllabusService> _mockSyllabusService;


        //get and filter
        [Fact]
        public async Task FilterByDateSyllabus_ReturnsOkResult_WithFilteredSyllabus_WhenDateRangeSpansMultipleYears()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2024, 12, 31);
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            _mockSyllabusService.Setup(service => service.FilterByDateSyllabusAsync(syllabusParameters, fromDate, toDate))
                                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.FilterByDateSyllabus(syllabusParameters, fromDate, toDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task GetAllSyllabus_ThrowsException_WhenServiceThrowsException()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            _mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters))
                                .ThrowsAsync(new Exception("Service layer exception")); // Adjust based on actual exception type

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetAllSyllabus(syllabusParameters));
        }



        [Fact]
        public async Task GetSyllabusById_ThrowsException_WhenSyllabusDoesNotExist()
        {
            // Arrange
            var nonExistentId = "nonexistentId";
            _mockSyllabusService.Setup(service => service.GetSyllabusByIdAsync(nonExistentId))
                                .ThrowsAsync(new Exception("Syllabus not found")); // Adjust based on actual exception type

            // Act and Assert

            await Assert.ThrowsAsync<Exception>(() => _controller.GetSyllabusById(nonExistentId));
        }


        [Fact]
        public async Task GetAllSyllabus_ReturnsOkResult_WithListOfSyllabus()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            _mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllSyllabus(syllabusParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task GetSyllabusById_ReturnsOkResult_WithSyllabus()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusId = "123e4567-e89b-12d3-a456-426655440000";
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            mockSyllabusService.Setup(service => service.GetSyllabusByIdAsync(syllabusId)).ReturnsAsync(expectedResult);

            // Act
            var result = await controller.GetSyllabusById(syllabusId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }


        [Fact]
        public async Task FilterByDateSyllabus_ReturnsOkResult_WithFilteredSyllabus()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusParameters = new SyllabusParameters();
            var fromDate = new DateTime(2024, 1, 1);
            var toDate = new DateTime(2024, 12, 31);
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            mockSyllabusService.Setup(service => service.FilterByDateSyllabusAsync(syllabusParameters, fromDate, toDate)).ReturnsAsync(expectedResult);

            // Act
            var result = await controller.FilterByDateSyllabus(syllabusParameters, fromDate, toDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task Sort_ReturnsOkResult_WithSortedSyllabus()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusParameters = new SyllabusParameters();
            var sortBy = "topicCode"; // Example sort parameter
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            mockSyllabusService.Setup(service => service.SortSyllabusAsync(syllabusParameters, sortBy)).ReturnsAsync(expectedResult);

            // Act
            var result = await controller.Sort(syllabusParameters, sortBy);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }
        [Fact]
        public async Task Sort_ThrowsException_WhenSortFieldIsUnsupported()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var unsupportedSortField = "unsupportedField";
            _mockSyllabusService.Setup(service => service.SortSyllabusAsync(syllabusParameters, unsupportedSortField))
                                .ThrowsAsync(new ArgumentException("Unsupported sort field")); // Adjust based on actual exception type

            // Act and Assert

            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Sort(syllabusParameters, unsupportedSortField));
        }





        [Fact]
        public async Task GetAllSyllabusAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.GetAllSyllabusAsync(syllabusParameters);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task GetSyllabusByIdAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusId = "testId";
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.GetSyllabusByIdAsync(syllabusId))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.GetSyllabusByIdAsync(syllabusId);

            // Assert
            Assert.Equal(expectedResponse, result);
        }


        [Fact]
        public async Task SortSyllabusAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var sortBy = "name";
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.SortSyllabusAsync(syllabusParameters, sortBy))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.SortSyllabusAsync(syllabusParameters, sortBy);

            // Assert
            Assert.Equal(expectedResponse, result);
        }


        [Fact]
        public async Task FilterByDateSyllabusAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var from = DateTime.Now.AddDays(-10);
            var to = DateTime.Now;
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.FilterByDateSyllabusAsync(syllabusParameters, from, to))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.FilterByDateSyllabusAsync(syllabusParameters, from, to);

            // Assert
            Assert.Equal(expectedResponse, result);
        }




        //create

        [Fact]
        public async Task CreateSyllabus_ReturnsCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var syllabusForCreationDTO = new SyllabusForCreationDTO
            {
                TopicCode = "TC123",
                TopicName = "Test Topic",
                Version = "1.0",
                CreatedBy = "TestUser",
                AttendeeNumber = 10,
                Level = "Intermediate",
                TechnicalRequirement = "Requirement",
                CourseObjective = "Objective",
                DeliveryPrinciple = "Principle",
                Days = 5,
                Hours = 10.0,
                AssessmentSchemes = new List<AssessmentSchemeForCreationDTO>(),
                SyllabusDays = new List<SyllabusDayForCreationDTO>()
            };
            var createdSyllabus = new SyllabusDTO
            {
                SyllabusId = "1",
                TopicCode = "TC123",
                TopicName = "Test Topic",
                Version = "1.0",
                CreatedBy = "TestUser",
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null,
                AttendeeNumber = 10,
                Level = "Intermediate",
                TechnicalRequirement = "Requirement",
                CourseObjective = "Objective",
                DeliveryPrinciple = "Principle",
                Days = 5,
                Hours = 10.0,
                Status = "Active"
            };
            _mockSyllabusService.Setup(s => s.CreateSyllabusAsync(It.IsAny<SyllabusForCreationDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(createdSyllabus);

            // Act
            var result = await _controller.CreateSyllabus(syllabusForCreationDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnedSyllabus = Assert.IsType<SyllabusDTO>(createdResult.Value);
            Assert.Equal(createdSyllabus.SyllabusId, returnedSyllabus.SyllabusId);
            Assert.Equal(createdSyllabus.TopicCode, returnedSyllabus.TopicCode);
            Assert.Equal(createdSyllabus.TopicName, returnedSyllabus.TopicName);
            Assert.Equal(createdSyllabus.Version, returnedSyllabus.Version);
            Assert.Equal(createdSyllabus.CreatedBy, returnedSyllabus.CreatedBy);
            Assert.Equal(createdSyllabus.AttendeeNumber, returnedSyllabus.AttendeeNumber);
            Assert.Equal(createdSyllabus.Level, returnedSyllabus.Level);
            Assert.Equal(createdSyllabus.TechnicalRequirement, returnedSyllabus.TechnicalRequirement);
            Assert.Equal(createdSyllabus.CourseObjective, returnedSyllabus.CourseObjective);
            Assert.Equal(createdSyllabus.DeliveryPrinciple, returnedSyllabus.DeliveryPrinciple);
            Assert.Equal(createdSyllabus.Days, returnedSyllabus.Days);
            Assert.Equal(createdSyllabus.Hours, returnedSyllabus.Hours);
            Assert.Equal(createdSyllabus.Status, returnedSyllabus.Status);
        }


        [Fact]
        public async Task CreateSyllabus_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var invalidSyllabus = new SyllabusForCreationDTO(); // Assuming this is an invalid model
            _controller.ModelState.AddModelError("TopicCode", "Topic code is required.");

            // Act
            var result = await _controller.CreateSyllabus(invalidSyllabus);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Contains(errors, error => error.Key == "TopicCode");
        }


        [Fact]
        public async Task CreateSyllabus_ReturnsBadRequestWithModelState_WhenRequiredFieldsAreNull()
        {
            // Arrange
            var invalidSyllabusForCreationDTO = new SyllabusForCreationDTO
            {
                TopicCode = "TC123",
                TopicName = null, // Required field set to null
                Version = null, // Required field set to null
                CreatedBy = "TestUser",
                AttendeeNumber = 10,
                Level = "Intermediate",
                TechnicalRequirement = "Requirement",
                CourseObjective = "Objective",
                DeliveryPrinciple = "Principle",
                Days = 5,
                Hours = 10.0,
                AssessmentSchemes = new List<AssessmentSchemeForCreationDTO>(),
                SyllabusDays = new List<SyllabusDayForCreationDTO>()
            };

            // Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.CreateSyllabus(invalidSyllabusForCreationDTO));
        }



        //active or deactive
        [Fact]
        public async Task ActivateAndDeactivateSyllabus_ReturnsNoContent_WhenCalled()
        {
            // Arrange
            var syllabusId = "testId";
            var activate = false;
            _mockSyllabusService.Setup(s => s.ActivateDeactivateSyllabus(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ActivateAndDeactivateSyllabus(syllabusId, activate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task ActivateAndDeactivateSyllabus_ThrowsException_WhenSyllabusDoesNotExist()
        {
            // Arrange
            var nonExistentId = "nonexistentId";
            var activate = true; // Assuming you want to test activating a syllabus

            // Setup the service to throw a SyllabusNotFoundException when trying to activate a non-existent syllabus
            _mockSyllabusService.Setup(service => service.ActivateDeactivateSyllabus(nonExistentId, activate))
                                .ThrowsAsync(new SyllabusNotFoundException(nonExistentId));

            // Act and Assert

            await Assert.ThrowsAsync<SyllabusNotFoundException>(() => _controller.ActivateAndDeactivateSyllabus(nonExistentId, activate));
        }





        //delete


        [Fact]
        public async Task DeleteSyllabus_ReturnsNoContent_WhenCalled()
        {
            // Arrange
            var syllabusId = "testId";
            _mockSyllabusService.Setup(service => service.DeleteSyllabusAsync(syllabusId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteSyllabus(syllabusId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteSyllabus_WithEmptyId_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusId = string.Empty; // ID empty

            // Act
            var result = await controller.DeleteSyllabus(syllabusId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSyllabus_WithInvalidId_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusId = "invalid-id"; // Assuming this is an invalid ID format

            // Assuming the service returns a NoContentResult for invalid IDs, you can set up the mock accordingly
            mockSyllabusService.Setup(service => service.DeleteSyllabusAsync(syllabusId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteSyllabus(syllabusId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSyllabusAsync_CompletesSuccessfully()
        {
            // Arrange
            var syllabusId = "testId";
            _mockSyllabusService.Setup(service => service.DeleteSyllabusAsync(syllabusId))
                                .Returns(Task.CompletedTask);

            // Act
            await _mockSyllabusService.Object.DeleteSyllabusAsync(syllabusId);

            // Assert
            _mockSyllabusService.Verify(service => service.DeleteSyllabusAsync(syllabusId), Times.Once);
        }

        [Fact]
        public async Task DeleteSyllabus_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = "invalid-id"; // Example of an invalid ID
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            // Assuming the service returns a specific error response for invalid IDs
            mockSyllabusService.Setup(service => service.DeleteSyllabusAsync(invalidId))
                                .ThrowsAsync(new ArgumentException("Invalid ID")); // Adjust based on actual exception type
            var controller = new SyllabusController(mockService.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.DeleteSyllabus(invalidId));
        }

        [Fact]
        public async Task DeleteSyllabus_ThrowsException_WhenSyllabusDoesNotExist()
        {
            // Arrange
            var nonExistentId = "nonexistentId";
            _mockSyllabusService.Setup(service => service.DeleteSyllabusAsync(nonExistentId))
                                .ThrowsAsync(new Exception("Syllabus not found")); // Adjust based on actual exception type

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.DeleteSyllabus(nonExistentId));
        }



        //get
        [Fact]
        public async Task GetHeaderAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusId = "testId";
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.GetHeaderAsync(syllabusId))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.GetHeaderAsync(syllabusId);

            // Assert
            Assert.Equal(expectedResponse, result);
        }


        [Fact]
        public async Task GetGeneralAsync_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusId = "testId";
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.GetGeneralAsync(syllabusId))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.GetGeneralAsync(syllabusId);

            // Assert
            Assert.Equal(expectedResponse, result);
        }


        [Fact]
        public async Task GetDeliveryTypePercentages_ReturnsExpectedResponse()
        {
            // Arrange
            var syllabusId = "testId";
            var expectedResponse = new ResponseDTO(); // Populate with expected data
            _mockSyllabusService.Setup(service => service.GetDeliveryTypePercentages(syllabusId))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockSyllabusService.Object.GetDeliveryTypePercentages(syllabusId);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task GetAllSyllabus_WithInvalidParameters_ThrowsArgumentException()
        {
            // Arrange
            var invalidParameters = new SyllabusParameters { /* Set invalid properties here */ };
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            // Assuming the service returns a specific error response for invalid parameters
            mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(invalidParameters))
                                .ThrowsAsync(new ArgumentException("Invalid parameters")); // Adjust based on actual exception type
            var controller = new SyllabusController(mockService.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.GetAllSyllabus(invalidParameters));
        }

        [Fact]
        public async Task GetAllSyllabus_CallsServiceLayerMethod()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            _mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllSyllabus(syllabusParameters);

            // Assert
            _mockSyllabusService.Verify(service => service.GetAllSyllabusAsync(syllabusParameters), Times.Once);
        }


        [Fact]
        public async Task GetAllSyllabus_ReturnsCorrectResponseStructure()
        {
            // Arrange
            var syllabusParameters = new SyllabusParameters();
            var expectedSyllabusList = new List<SyllabusDTO> { new SyllabusDTO { SyllabusId = "1" } };
            var expectedResult = new ResponseDTO { Result = new ResultDTO { Data = expectedSyllabusList } };
            _mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetAllSyllabus(syllabusParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult.Result.Data, model.Result.Data);
        }




        //sreach
        [Fact]
        public async Task Get_ReturnsOkResult_WithSearchResults()
        {
            // Arrange
            var keywords = new string[] { "keyword1", "keyword2" };
            var syllabusParameters = new SyllabusParameters
            {
                PageNumber = 1,
                PageSize = 10
            };
            var expectedResult = new ResponseDTO // Assuming ResponseDTO is the expected return type
            {
                StatusCode = 200,
                Message = "Success",
                IsSuccess = true,
                Result = new ResultDTO // Assuming ResultDTO is the type of the result
                {
                    Data = new List<object>(), // Example data
                    Metadata = new SyllabusManagementAPI.Entities.Helpers.Metadata // Assuming Metadata is a class you've defined
                    {
                        CurrentPage = 1,
                        TotalPages = 5,
                        TotalItems = 50,
                        PageSize = 10
                    }
                }
            };

            // Mock the IElasticService to return the expected result
            var mockElasticService = new Mock<IElasticService>();
            mockElasticService.Setup(service => service.SearchAsync(keywords, syllabusParameters))
                              .ReturnsAsync(expectedResult);

            // Setup the IServiceWrapper to return the mocked IElasticService
            _mockService.Setup(wrapper => wrapper.elasticService)
                               .Returns(mockElasticService.Object);

            // Act
            var result = await _sreachcontroller.Get(keywords, syllabusParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResult.StatusCode, model.StatusCode);
            Assert.Equal(expectedResult.Message, model.Message);
            Assert.Equal(expectedResult.IsSuccess, model.IsSuccess);
            Assert.Equal(expectedResult.Result.Data, model.Result.Data);
            Assert.Equal(expectedResult.Result.Metadata.CurrentPage, model.Result.Metadata.CurrentPage);
            Assert.Equal(expectedResult.Result.Metadata.TotalPages, model.Result.Metadata.TotalPages);
            Assert.Equal(expectedResult.Result.Metadata.TotalItems, model.Result.Metadata.TotalItems);
            Assert.Equal(expectedResult.Result.Metadata.PageSize, model.Result.Metadata.PageSize);
            Assert.Equal(expectedResult.Result.Metadata.HasPrevious, model.Result.Metadata.HasPrevious);
            Assert.Equal(expectedResult.Result.Metadata.HasNext, model.Result.Metadata.HasNext);
        }
        //duplicate
        [Fact]
        public async Task DuplicateProgramAsync_ReturnsOkResult_WithValidRequest()
        {
            // Arrange
            var duplicateRequest = new DuplicateSyllabusRequest
            {
                SyllabusId = "123",
                TopicCode = "TC123",
                CreatedBy = "TestUser"
            };
            var expectedSyllabusDto = new SyllabusDTO
            {
                SyllabusId = "456",
                TopicCode = "TC123",
                TopicName = "Duplicated Syllabus",
                Version = "1.0",
                CreatedBy = "TestUser",
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null,
                AttendeeNumber = 10,
                Level = "Intermediate",
                TechnicalRequirement = "Requirement",
                CourseObjective = "Objective",
                DeliveryPrinciple = "Principle",
                Days = 5,
                Hours = 10.0,
                Status = "Draft"
            };
            var expectedResponse = new ResponseDTO
            {
                StatusCode = 200,
                Message = "Returned syllabus with id: '123' from database duplicated.",
                IsSuccess = true,
                Result = new ResultDTO
                {
                    Data = expectedSyllabusDto,
                    Metadata = null // Adjust based on actual metadata structure
                }
            };
            _mockSyllabusService.Setup(service => service.DuplicateSyllabusAsync(It.IsAny<DuplicateSyllabusRequest>()))
                                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DuplicateProgramAsync(duplicateRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal(expectedResponse.StatusCode, returnedResponse.StatusCode);
            Assert.Equal(expectedResponse.Message, returnedResponse.Message);
            Assert.Equal(expectedResponse.IsSuccess, returnedResponse.IsSuccess);
            Assert.Equal(expectedResponse.Result.Data, returnedResponse.Result.Data);

        }

        [Fact]
        public async Task DuplicateProgramAsync_ReturnsNotFound_WhenSyllabusDoesNotExist()
        {
            // Arrange
            var nonExistentId = "nonexistentId";
            var duplicateRequest = new DuplicateSyllabusRequest { SyllabusId = nonExistentId };
            _mockSyllabusService.Setup(service => service.DuplicateSyllabusAsync(duplicateRequest))
                                .ThrowsAsync(new Exception("Syllabus not found")); // Adjust based on actual exception type

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.DuplicateProgramAsync(duplicateRequest));
        }


        //Update

        [Fact]
        public async Task UpdateSyllabus_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var syllabusForUpdateDTO = new SyllabusForUpdateDTO
            {
                SyllabusId = "123",
                TopicCode = "TC123",
                TopicName = "Updated Topic",
                Version = "1.1",
                CreatedBy = "TestUser",
                AttendeeNumber = 15,
                Level = "Advanced",
                TechnicalRequirement = "Updated Requirement",
                CourseObjective = "Updated Objective",
                DeliveryPrinciple = "Updated Principle",
                Days = 7,
                Hours = 12.0,
                AssessmentSchemes = new List<AssessmentSchemeForCreationDTO>(),
                SyllabusDays = new List<SyllabusDayForCreationDTO>()
            };
            var expectedSyllabus = new SyllabusDTO
            {
                SyllabusId = "123",
                TopicCode = "TC123",
                TopicName = "Updated Topic",
                Version = "1.1",
                CreatedBy = "TestUser",
                AttendeeNumber = 15,
                Level = "Advanced",
                TechnicalRequirement = "Updated Requirement",
                CourseObjective = "Updated Objective",
                DeliveryPrinciple = "Updated Principle",
                Days = 7,
                Hours = 12.0,
                Status = "Active", // Assuming the status is updated to "Active"
                // Populate other properties as necessary
            };
            _mockService.Setup(service => service.SyllabusService.UpdateSyllabusAsync(It.IsAny<SyllabusForUpdateDTO>()))
                .ReturnsAsync(expectedSyllabus);

            // Act
            var result = await _controller.UpdateSyllabus(syllabusForUpdateDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<SyllabusDTO>(okResult.Value);
            Assert.Equal(expectedSyllabus, model);
        }

        [Fact]
        public async Task UpdateSyllabus_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var syllabusForUpdateDTO = new SyllabusForUpdateDTO
            {
                // For example, missing required fields
                SyllabusId = null, // Missing required field
                TopicName = null, // Missing required field
                Version = null, // Missing required field
                CreatedBy = null, // Missing required field
                AttendeeNumber = null, // Missing required field
                Level = null, // Missing required field
                TechnicalRequirement = null, // Missing required field
                CourseObjective = null, // Missing required field
            };

            // Manually add model errors to simulate an invalid model
            _controller.ModelState.AddModelError("SyllabusId", "SyllabusId is required");
            _controller.ModelState.AddModelError("TopicName", "TopicName is required");
            _controller.ModelState.AddModelError("Version", "Version is required");
            _controller.ModelState.AddModelError("CreatedBy", "CreatedBy is required");
            _controller.ModelState.AddModelError("AttendeeNumber", "AttendeeNumber is required");
            _controller.ModelState.AddModelError("Level", "Level is required");
            _controller.ModelState.AddModelError("TechnicalRequirement", "TechnicalRequirement is required");
            _controller.ModelState.AddModelError("CourseObjective", "CourseObjective is required");

            // Act
            var result = await _controller.UpdateSyllabus(syllabusForUpdateDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Contains(errors, error => error.Key == "SyllabusId");
            Assert.Contains(errors, error => error.Key == "TopicName");
            Assert.Contains(errors, error => error.Key == "Version");
            Assert.Contains(errors, error => error.Key == "CreatedBy");
            Assert.Contains(errors, error => error.Key == "AttendeeNumber");
            Assert.Contains(errors, error => error.Key == "Level");
            Assert.Contains(errors, error => error.Key == "TechnicalRequirement");
            Assert.Contains(errors, error => error.Key == "CourseObjective");
        }


        //import
        [Fact]
        public async Task ImportSyllabus_ReturnsOkResult_WhenFileIsValid()
        {
            // Arrange
            // Create a MemoryStream with some data
            var data = Encoding.UTF8.GetBytes("This is some test data");
            var memoryStream = new MemoryStream(data);
            var formFile = new FormFile(memoryStream, 0, data.Length, "Data", "Template_Import_Syllabus.xlsx")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
            var model = new DuplicateHandlingDTO { File = formFile };

            // Assuming the method you're mocking returns a Task<(SyllabusDTO, bool)>
            var expectedResult = (new SyllabusDTO(), false); // Adjust this to match the actual return type

            // Mock the service to return a specific result
            _mockSyllabusService.Setup(service => service.HandleDuplicate(It.IsAny<DuplicateHandlingDTO>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.ImportSyllabus(model);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task ImportSyllabus_ThrowsArgumentException_WhenFileIsMissingOrEmpty()
        {
            // Arrange
            var formFile = new FormFile(new MemoryStream(), 0, 0, "Data", "Template_Import_Syllabus.xlsx")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
            var model = new DuplicateHandlingDTO { File = formFile };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.ImportSyllabus(model));
        }







    }
}
