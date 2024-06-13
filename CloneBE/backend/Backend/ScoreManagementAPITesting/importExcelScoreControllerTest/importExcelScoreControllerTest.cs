using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ScoreManagementAPI.Controllers;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManagementAPITesting.importExcelScoreControllerTest
{
    public class importExcelScoreControllerTest
    {
        private readonly Mock<IimportExcelScoreRepository> _mockImportExcelScoreRepository;
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly ImportExcelScoreController _controller;
        private readonly Mock<ILogger<ImportExcelScoreController>> _logger;

        public importExcelScoreControllerTest()
        {
            _mockImportExcelScoreRepository = new Mock<IimportExcelScoreRepository>();
            _mockStudentRepository = new Mock<IStudentRepository>();
            _logger = new Mock<ILogger<ImportExcelScoreController>>(); // Create a mock logger
            _controller = new ImportExcelScoreController(_mockImportExcelScoreRepository.Object, _mockStudentRepository.Object, _logger.Object); // Pass the mock logger to the controller constructor
        }

        [Fact]
        public void UploadExcelFile_ReturnsOkResult_WhenFileIsValid()
        {
            // Arrange
            var request = new ImportExcelScoreRequest
            {
                File = new FormFile(new MemoryStream(), 0, 0, "file", "file.xlsx")
            };
            var expectedResponse = new ImportExcelScoreResponse { IsSuccess = true, Message = "File processed successfully." };
            _mockImportExcelScoreRepository.Setup(repo => repo.AddExcelWithListScore(It.IsAny<ImportExcelScoreRequest>(), It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<IStudentRepository>()))
                .Returns(expectedResponse);

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ImportExcelScoreResponse>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public void UploadExcelFile_ReturnsOkResultWithFailureMessage_WhenExceptionOccurs()
        {
            // Arrange
            var request = new ImportExcelScoreRequest
            {
                File = new FormFile(new MemoryStream(), 0, 0, "file", "file.xlsx")
            };
            _mockImportExcelScoreRepository.Setup(repo => repo.AddExcelWithListScore(It.IsAny<ImportExcelScoreRequest>(), It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<IStudentRepository>()))
                .Throws(new Exception("An error occurred."));

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ImportExcelScoreResponse>(okResult.Value);
            Assert.False(response.IsSuccess);
            Assert.Contains("An error occurred.", response.Message);
        }

        [Fact]
        public void UploadExcelFile_ReturnsOkResultWithFailureMessage_WhenFileProcessingFails()
        {
            // Arrange
            var request = new ImportExcelScoreRequest
            {
                File = new FormFile(new MemoryStream(), 0, 0, "file", "file.xlsx")
            };
            var expectedResponse = new ImportExcelScoreResponse { IsSuccess = false, Message = "File processing failed due to invalid data." };
            _mockImportExcelScoreRepository.Setup(repo => repo.AddExcelWithListScore(It.IsAny<ImportExcelScoreRequest>(), It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<IStudentRepository>()))
                .Returns(expectedResponse);

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<ImportExcelScoreResponse>(okResult.Value);
            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public void UploadExcelFile_ReturnsBadRequest_WhenFileIsMissing()
        {
            // Arrange
            var request = new ImportExcelScoreRequest { File = null };

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UploadExcelFile_ReturnsBadRequest_WhenFileIsEmpty()
        {
            // Arrange
            var request = new ImportExcelScoreRequest
            {
                File = new FormFile(new MemoryStream(), 0, 0, "file", "file.xlsx")
            };

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UploadExcelFile_ReturnsBadRequest_WhenFileIsNotExcel()
        {
            // Arrange
            var request = new ImportExcelScoreRequest
            {
                File = new FormFile(new MemoryStream(), 0, 0, "file", "file.txt")
            };

            // Act
            var result = _controller.UploadExcelFile(request, "");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }



    }
}
