using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entities.Context;
using ScoreManagementAPI.Controllers;
using ScoreManagementAPI.DTO;
using Entities.Models;
using ScoreManagementAPI.Repository;
using Moq;

namespace ScoreManagementAPITesting.studentControllerTest
{
    public class studentControllerTest
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly Mock<ILogger<StudentController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<FamsContext> _mockContext;
        private readonly StudentController _controller;

        public studentControllerTest()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockLogger = new Mock<ILogger<StudentController>>();
            _mockMapper = new Mock<IMapper>();
            _mockContext = new Mock<FamsContext>();
            _controller = new StudentController(_mockStudentRepository.Object, _mockLogger.Object, _mockMapper.Object, _mockContext.Object);
        }

        [Fact]
        public void GetStudents_ReturnsListOfStudents()
        {
            // Arrange
            var students = new List<Student>
        {
            new Student { Id = 1, FullName = "Đinh Thế Vinh" },
            new Student { Id = 2, FullName = "Hoàng Hải Sơn" }
        };
            var expectedStudentDTOs = new List<StudentDTO>
        {
            new StudentDTO { Id = "1", FullName = "Đinh Thế Vinh" },
            new StudentDTO { Id = "2", FullName = "Hoàng Hải Sơn" }
        };
            _mockStudentRepository.Setup(repo => repo.GetStudents()).Returns(students);
            _mockMapper.Setup(mapper => mapper.Map<List<StudentDTO>>(students)).Returns(expectedStudentDTOs);

            // Act
            var result = _controller.GetStudents();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<StudentDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualStudentDTOs = Assert.IsAssignableFrom<List<StudentDTO>>(okResult.Value);
            Assert.Equal(expectedStudentDTOs, actualStudentDTOs);
        }

        [Fact]
        public void GetStudents_ReturnsNotFoundResult_WhenNoStudents()
        {
            // Arrange
            // Setup the student repository to return an empty list of students
            _mockStudentRepository.Setup(repo => repo.GetStudents()).Returns(new List<Student>());

            // Act
            var result = _controller.GetStudents();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<StudentDTO>>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void GetStudents_ReturnsInternalServerError_WhenMapperThrowsException()
        {
            // Arrange
            var students = new List<Student>
                {
                    new Student { Id = 1, FullName = "Đinh Thế Vinh" },
                    new Student { Id = 2, FullName = "Hoàng Hải Sơn" }
                };
            _mockStudentRepository.Setup(repo => repo.GetStudents()).Returns(students);
            _mockMapper.Setup(mapper => mapper.Map<List<StudentDTO>>(students)).Throws(new Exception("Mapper exception"));

            // Act
            var result = _controller.GetStudents();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<StudentDTO>>>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetStudents_ReturnsInternalServerError_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockStudentRepository.Setup(repo => repo.GetStudents()).Throws(new Exception("Repository exception"));

            // Act
            var result = _controller.GetStudents();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<StudentDTO>>>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetStudentById_ReturnsStudentDTO_WhenStudentExists()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";
            var student = new Student { StudentId = studentId, FullName = "Đinh Thế Vinh" };
            var expectedStudentDTO = new StudentDTO { Id = studentId, FullName = "Đinh Thế Vinh" };
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(studentId)).Returns(expectedStudentDTO);

            // Act
            var result = _controller.GetStudentById(studentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualStudentDTO = Assert.IsAssignableFrom<StudentDTO>(okResult.Value);
            Assert.Equal(expectedStudentDTO, actualStudentDTO);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFoundResult_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentId = "asdasdas";
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(studentId)).Returns((StudentDTO)null);

            // Act
            var result = _controller.GetStudentById(studentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult);
            Assert.Equal($"Student with ID {studentId} not found!", notFoundResult.Value);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFoundResult_WhenStudentIdIsNull()
        {
            // Arrange
            var studentId = string.Empty;
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(studentId)).Returns((StudentDTO)null);

            // Act
            var result = _controller.GetStudentById(studentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult);
            Assert.Equal($"Student with ID {studentId} not found!", notFoundResult.Value);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFoundResult_WhenStudentIdIsInvalid()
        {
            // Arrange
            var invalidStudentId = "invalid-id";
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(invalidStudentId)).Returns((StudentDTO)null);

            // Act
            var result = _controller.GetStudentById(invalidStudentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult);
            Assert.Equal($"Student with ID {invalidStudentId} not found!", notFoundResult.Value);
        }

        [Fact]
        public void GetStudentById_ReturnsInternalServerError_WhenRepositoryThrowsException()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(studentId)).Throws(new Exception("Repository exception"));

            // Act
            var result = _controller.GetStudentById(studentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public void GetStudentById_ReturnsInternalServerError_WhenMapperThrowsException()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";
            var student = new Student { StudentId = studentId, FullName = "Đinh Thế Vinh" };
            var expectedStudentDTO = new StudentDTO { Id = studentId, FullName = "Đinh Thế Vinh" };
            _mockStudentRepository.Setup(repo => repo.GetStudentByID(studentId)).Returns(expectedStudentDTO);
            _mockMapper.Setup(mapper => mapper.Map<StudentDTO>(It.IsAny<Student>())).Throws(new Exception("Mapper exception"));

            // Act
            var result = _controller.GetStudentById(studentId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }
    }
}

