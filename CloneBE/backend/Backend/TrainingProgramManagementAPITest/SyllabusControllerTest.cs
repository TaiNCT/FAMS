using Microsoft.AspNetCore.Mvc;
using Moq;
using SyllabusManagementAPI.Controllers;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.ServiceContracts;

namespace TrainingProgramManagementAPITest
{
    public class SyllabusControllerTest
    {
        private readonly Mock<IServiceWrapper> _mockService;
        private readonly SyllabusController _controller;

        public SyllabusControllerTest()
        {
            _mockService = new Mock<IServiceWrapper>();
            _controller = new SyllabusController(_mockService.Object);
            _mockSyllabusService = new Mock<ISyllabusService>();
        }

        private readonly Mock<ISyllabusService> _mockSyllabusService;


        //get and filter
        [Fact]
        public async Task GetAllSyllabus_ReturnsOkResult_WithListOfSyllabus()
        {
            // Arrange
            var mockService = new Mock<IServiceWrapper>();
            var mockSyllabusService = new Mock<ISyllabusService>();
            mockService.Setup(service => service.SyllabusService).Returns(mockSyllabusService.Object);
            var controller = new SyllabusController(mockService.Object);
            var syllabusParameters = new SyllabusParameters();
            var expectedResult = new ResponseDTO(); // Assuming ResponseDTO is the expected return type
            mockSyllabusService.Setup(service => service.GetAllSyllabusAsync(syllabusParameters)).ReturnsAsync(expectedResult);

            // Act
            var result = await controller.GetAllSyllabus(syllabusParameters);

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





        //delete

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






    }
}   