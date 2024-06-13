using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.Controllers;
using ScoreManagementAPI.Repository;
using ScoreManagementAPI.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Entities.Context;
using Microsoft.AspNetCore.Http;

namespace ScoreManagementAPITesting.studentUpdateControllerTest
{
    public class scoreUpdateControllerTest
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly Mock<ILogger<StudentController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<FamsContext> _mockContext;
        private readonly ScoreUpdateController _controller;

        public scoreUpdateControllerTest()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockLogger = new Mock<ILogger<StudentController>>();
            _mockMapper = new Mock<IMapper>();
            _mockContext = new Mock<FamsContext>();

            _controller = new ScoreUpdateController(_mockStudentRepository.Object, _mockLogger.Object, _mockMapper.Object, _mockContext.Object);
        }

        [Fact]
        public void UpdateStudentScore_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";
            var studentUpdateData = new IStudentUpdate
            {
                id = studentId,
                name = "Đinh Thế Vinh",
                gender = 1,
                dob = DateTime.Parse("2024-02-15T03:33:55.945Z"),
                phone = "121-233-2312",
                email = "vinh11@fpt.edu.vn",
                location = "Chicago, CG",
                university = "Greenwich 2 University",
                major = "Computer Science",
                gpa = 100,
                recer = "Can Tho",
                gradtime = DateTime.Parse("2024-05-29T03:33:55.945Z"),
                html = 90.0,
                css = 85.0,
                quiz3 = 80.0,
                quiz4 = 75.0,
                quiz5 = 70.0,
                quiz6 = 65.0,
                practice1 = 90.0,
                practice2 = 85.0,
                practice3 = 80.0,
                mock = 95.0,
                final = 92.0,
                gpa2 = 100,
                level = 4
            };

            var student = new Student
            {
                MutatableStudentId = "STD48106",
                StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975",
                FullName = "Đinh Thế Vinh"
            };

            // Create an in-memory collection of students
            var students = new List<Student> { student }.AsQueryable();

            // Mock the DbSet to behave like an in-memory collection
            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            // Mock the context to return the mocked DbSet
            _mockContext.Setup(c => c.Students).Returns(mockSet.Object);

            // Act
            var result = _controller.UpdateStudentScore(studentId, studentUpdateData);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void UpdateStudentScore_ReturnsNotFoundResult_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentId = "nonexistentId";
            var studentUpdateData = new IStudentUpdate
            {
                id = studentId,
                name = "Đinh Thế Vinh",
                gender = 1,
                dob = DateTime.Parse("2024-02-15T03:33:55.945Z"),
                phone = "121-233-2312",
                email = "vinh11@fpt.edu.vn",
                location = "Chicago, CG",
                university = "Greenwich 2 University",
                major = "Computer Science",
                gpa = 100,
                recer = "Can Tho",
                gradtime = DateTime.Parse("2024-05-29T03:33:55.945Z"),
                html = 90.0,
                css = 85.0,
                quiz3 = 80.0,
                quiz4 = 75.0,
                quiz5 = 70.0,
                quiz6 = 65.0,
                practice1 = 90.0,
                practice2 = 85.0,
                practice3 = 80.0,
                mock = 95.0,
                final = 92.0,
                gpa2 = 100,
                level = 4
            };

            // Create an in-memory collection of students without the student with the specified ID
            var students = new List<Student>().AsQueryable();

            // Mock the DbSet to behave like an in-memory collection
            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            // Mock the context to return the mocked DbSet
            _mockContext.Setup(c => c.Students).Returns(mockSet.Object);

            // Act
            var result = _controller.UpdateStudentScore(studentId, studentUpdateData);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void UpdateStudentScore_ReturnsBadRequestResult_WhenInputDataIsInvalid()
        {
            // Arrange
            var studentId = string.Empty; // Invalid ID
            var studentUpdateData = new IStudentUpdate
            {
                id = studentId,
                name = "Đsinh Thế Vinh",
                gender = 1,
                dob = DateTime.Parse("2014-02-15T03:33:55.945Z"),
                phone = "123-233-2312",
                email = "vinh211@fpt.edu.vn",
                location = "Chicago, CG",
                university = "Greenwich 21 University",
                major = "Computers Science",
                gpa = 100,
                recer = "",
                gradtime = DateTime.Parse("2024-05-29T03:33:55.945Z"),
                html = 90.0,
                css = 85.0,
                quiz3 = 80.0,
                quiz4 = 75.0,
                quiz5 = 70.0,
                quiz6 = 65.0,
                practice1 = 90.0,
                practice2 = 85.0,
                practice3 = 80.0,
                mock = 95.0,
                final = 92.0,
                gpa2 = 100,
                level = 4
            };

            var students = new List<Student>().AsQueryable();

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            _mockContext.Setup(c => c.Students).Returns(mockSet.Object);

            // Act
            var result = _controller.UpdateStudentScore(studentId, studentUpdateData);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void UpdateStudentScore_ReturnsInternalServerErrorResult_WhenDatabaseErrorOccurs()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";
            var studentUpdateData = new IStudentUpdate
            {
                id = studentId,
                name = "Đinh Thế Vinh",
                gender = 1,
                dob = DateTime.Parse("2024-02-15T03:33:55.945Z"),
                phone = "121-233-2312",
                email = "vinh11@fpt.edu.vn",
                location = "Chicago, CG",
                university = "Greenwich 2 University",
                major = "Computer Science",
                gpa = 100,
                recer = "Can Tho",
                gradtime = DateTime.Parse("2024-05-29T03:33:55.945Z"),
                html = 90.0,
                css = 85.0,
                quiz3 = 80.0,
                quiz4 = 75.0,
                quiz5 = 70.0,
                quiz6 = 65.0,
                practice1 = 90.0,
                practice2 = 85.0,
                practice3 = 80.0,
                mock = 95.0,
                final = 92.0,
                gpa2 = 100,
                level = 4
            };

            var students = new List<Student>
    {
        new Student {
            StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975",
        },
    }.AsQueryable();

            // Mock the DbSet
            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            // Mock the FamsContext
            _mockContext.Setup(m => m.Students).Returns(mockSet.Object);

            // Mock the repository to throw an exception
            _mockStudentRepository.Setup(r => r.UpdateStudentBasedOnJSON(It.IsAny<Student>(), It.IsAny<IStudentUpdate>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _controller.UpdateStudentScore(studentId, studentUpdateData);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
        }

        [Fact]
        public void UpdateStudentScore_ReturnsOkObjectResult_WhenStudentIdIsValid()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975"; // Assuming a valid StudentId
            var studentUpdateData = new IStudentUpdate
            {
                id = studentId,
                name = "Đinh Thế Vinh",
                gender = 1,
                dob = DateTime.Parse("2024-02-15T03:33:55.945Z"),
                phone = "121-233-2312",
                email = "vinh11@fpt.edu.vn",
                location = "Chicago, CG",
                university = "Greenwich 2 University",
                major = "Computer Science",
                gpa = 100,
                recer = "Can Tho",
                gradtime = DateTime.Parse("2024-05-29T03:33:55.945Z"),
                html = 90.0,
                css = 85.0,
                quiz3 = 80.0,
                quiz4 = 75.0,
                quiz5 = 70.0,
                quiz6 = 65.0,
                practice1 = 90.0,
                practice2 = 85.0,
                practice3 = 80.0,
                mock = 95.0,
                final = 92.0,
                gpa2 = 100,
                level = 4
            };

            // Create an in-memory collection of students
            var students = new List<Student>
            {
        new Student {
            StudentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975",
            // Add other properties as needed
        }
            }.AsQueryable();

            // Mock the DbSet to behave like an in-memory collection
            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            // Mock the context to return the mocked DbSet
            _mockContext.Setup(c => c.Students).Returns(mockSet.Object);

            // Act
            var result = _controller.UpdateStudentScore(studentId, studentUpdateData);

            // Assert
            Assert.IsType<OkObjectResult>(result); // Expect an OkObjectResult when the studentId is valid
        }

        [Fact]
        public void UpdateStudentScore_ReturnsBadRequest_WhenStudentUpdateDataIsNull()
        {
            // Arrange
            var studentId = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";

            // Act
            var result = _controller.UpdateStudentScore(studentId, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }
    }
}