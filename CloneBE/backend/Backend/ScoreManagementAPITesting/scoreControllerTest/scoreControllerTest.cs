using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScoreManagementAPI.Controllers;
using ScoreManagementAPI.DTO;
using Entities.Models;
using ScoreManagementAPI.Repository;
using static ScoreManagementAPI.Controllers.ScoreController;

namespace ScoreManagementAPITesting.scoreControlletTest
{
    public class scoreControllerTest
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IScoreRepository> _scoreRepository;

        public scoreControllerTest()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _scoreRepository = new Mock<IScoreRepository>();
        }

        [Fact]
        public void GetScoresByClassId_ReturnsOkResult_WithListOfScores()
        {
            // Arrange
            var mockScoreRepository = new Mock<IScoreRepository>();
            var mockStudentRepository = new Mock<IStudentRepository>();
            var classId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9";
            var expectedScores = new List<ScoreDTO>
            {
                new ScoreDTO { FullName = "William Wilson", uuid = "815dd6f4-2d41-4c6c-a032-5e78a1cf065b" }
            };

            mockScoreRepository.Setup(repo => repo.GetScoresByClassId(classId)).Returns(expectedScores);
            var controller = new ScoreController(mockStudentRepository.Object, mockScoreRepository.Object);

            // Act
            var result = controller.GetScoresByClassId(classId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ListScoreDTO>(okResult.Value);
            Assert.Equal(expectedScores.Count, model.count);
            Assert.Equal(expectedScores, model.data);
        }

        [Fact]
        public void GetScoresByClassId_ReturnsOkResult_WhenScoresExist()
        {
            // Arrange
            var mockScoreRepository = new Mock<IScoreRepository>();
            var mockStudentRepository = new Mock<IStudentRepository>();
            var classId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9";
            var scores = new List<ScoreDTO> { new ScoreDTO() };

            mockScoreRepository.Setup(repo => repo.GetScoresByClassId(classId)).Returns(scores);
            var controller = new ScoreController(mockStudentRepository.Object, mockScoreRepository.Object);

            // Act
            var result = controller.GetScoresByClassId(classId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result); // This test will fail because it expects NotFound when scores exist
        }

        [Fact]
        public void GetScoresByClassId_DoesNotVerifyScoreCount()
        {
            // Arrange
            var mockScoreRepository = new Mock<IScoreRepository>();
            var mockStudentRepository = new Mock<IStudentRepository>();
            var classId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9";
            var scores = new List<ScoreDTO> { new ScoreDTO(), new ScoreDTO() };

            mockScoreRepository.Setup(repo => repo.GetScoresByClassId(classId)).Returns(scores);
            var controller = new ScoreController(mockStudentRepository.Object, mockScoreRepository.Object);

            // Act
            var result = controller.GetScoresByClassId(classId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ListScoreDTO>(okResult.Value);
            // This test will pass even if the count is incorrect because it does not assert the count
        }

        [Fact]
        public void GetScoresByClassId_ReturnsIsSuccessTrue_WhenScoresExist()
        {
            // Arrange
            var mockScoreRepository = new Mock<IScoreRepository>();
            var mockStudentRepository = new Mock<IStudentRepository>();
            var classId = "04b2851a-5d19-4aa2-aa14-f8d68d0c90b9";
            var scores = new List<ScoreDTO> { new ScoreDTO() };

            mockScoreRepository.Setup(repo => repo.GetScoresByClassId(classId)).Returns(scores);
            var controller = new ScoreController(mockStudentRepository.Object, mockScoreRepository.Object);

            // Act
            var result = controller.GetScoresByClassId(classId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ListScoreDTO>(okResult.Value);
            Assert.True(model.IsSuccess); // This test will fail because IsSuccess should be true when scores exist
        }     
    }
}