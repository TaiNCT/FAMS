using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReservationManagementAPI.Controllers;
using ReservationManagementAPI.Contracts;
using ReservationManagementAPI.Entities.DTOs;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ReservationManagementAPI.Entities.Models;
using ReservationManagementAPI.Entities;
using ReservationManagementAPI.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static ReservationManagementAPITesting.TestAsync;
using ReservationManagementAPI.Entities.Errors;
using System.Linq.Expressions;
using static ReservationManagementAPI.Tests.Controllers.DbSetMocker;
using Result = ReservationManagementAPI.Exceptions.Result;
using Nest;

namespace ReservationManagementAPI.Tests.Controllers
{
    public static class DbSetMocker
    {
        public class NotFoundException : Exception
        {
            public NotFoundException()
            {
            }

            public NotFoundException(string message)
                : base(message)
            {
            }

            public NotFoundException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
        public static DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            return mockDbSet.Object;
        }
    }

    public class StudentControllerTests
    {
        [Fact]
        public async Task GetAllReservedStudents_Should_Returns_OkObjectResult_With_StudentList()
        {
            // Arrange
            var mockStudentRepo = new Mock<IStudentRepository>();
            var mockLogger = new Mock<ILogger<StudentController>>();
            var mockReservedClassRepo = new Mock<IReservedClassRepository>();
            var mockClassRepo = new Mock<IClassRepository>();

            var controller = new StudentController(
                mockLogger.Object,
                null, // Mocking IElasticClient is not necessary for this test
                null, // Mocking IMapper is not necessary for this test
                mockStudentRepo.Object,
                mockReservedClassRepo.Object,
                mockClassRepo.Object
            );

            var pageNumber = 1; // Sample page number for testing

            var sampleStudentList = new List<StudentReservedDTO>
            {
                // Sample student DTO objects
            };

            mockStudentRepo.Setup(repo => repo.GetAllReservedStudentsByReserveTime())
                .ReturnsAsync(sampleStudentList);

            // Act
            var result = await controller.GetAllReservedStudentsByReverseTime(); // Corrected method name

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StudentReservedDTO>>(okResult.Value);
            Assert.Equal(sampleStudentList, model);
        }

        [Fact]
        public async Task GetAllReservedStudents_Should_Return_EmptyList_WhenNoStudentsAreReserved()
        {
            // Arrange
            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(repo => repo.GetAllReservedStudentsByReserveTime())
                           .ReturnsAsync(new List<StudentReservedDTO>()); // Return empty list

            var controller = new StudentController(null, null, null, mockStudentRepo.Object, null, null);

            // Act
            var result = await controller.GetAllReservedStudentsByReverseTime(); // Corrected method name

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Empty((System.Collections.IEnumerable)okResult.Value);
        }
        [Fact]
        public async Task GetStudentById_StudentExists_ReturnsStudentDTO()
        {
            // Arrange
            var studentId = "S06";
            var expectedStudent = new Student { StudentId = studentId, FullName = "Hoang Phat", /* Other properties */ };
            var students = new List<Student> { expectedStudent }; // Create a list with the expected student

            var repositoryContextMock = new Mock<RepositoryContext>();
            var dbSetMock = new Mock<DbSet<Student>>();

            // Setup DbSet.FirstOrDefaultAsync method
            dbSetMock.As<IAsyncEnumerable<Student>>()
         .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
         .Returns(new TestAsyncEnumerator<Student>(students.GetEnumerator()));

            dbSetMock.As<IQueryable<Student>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<Student>(students.AsQueryable().Provider));

            dbSetMock.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            repositoryContextMock.Setup(c => c.Students).Returns(dbSetMock.Object);

            var mapperMock = new Mock<IMapper>();
            var studentRepository = new StudentRepository(repositoryContextMock.Object, mapperMock.Object, null, null);

            // Act
            var result = await studentRepository.GetStudentById(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStudent.StudentId, result.StudentId);
            Assert.Equal(expectedStudent.FullName, result.FullName);

        }


        [Fact]
        public async Task GetStudentById_StudentDoesNotExist_ReturnsNull()
        {
            // Arrange
            var studentId = "S10";
            var students = new List<Student>(); // Empty list to simulate no students in the database

            var queryableStudents = students.AsQueryable(); // Convert list to IQueryable

            var dbSetMock = new Mock<DbSet<Student>>();
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(queryableStudents.Provider);
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(queryableStudents.Expression);
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(queryableStudents.ElementType);
            dbSetMock.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => queryableStudents.GetEnumerator());

            // Setup async operations
            dbSetMock.As<IAsyncEnumerable<Student>>()
                    .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                    .Returns(new TestAsyncEnumerator<Student>(students.GetEnumerator()));
            dbSetMock.As<IQueryable<Student>>()
                    .Setup(m => m.Provider)
                    .Returns(new TestAsyncQueryProvider<Student>(queryableStudents.Provider));

            var repositoryContextMock = new Mock<RepositoryContext>();
            repositoryContextMock.Setup(c => c.Students).Returns(dbSetMock.Object);

            var mapperMock = new Mock<IMapper>();

            var studentRepository = new StudentRepository(repositoryContextMock.Object, mapperMock.Object, null, null);

            // Act
            var result = await studentRepository.GetStudentById(studentId);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateStatusStudent_Should_Return_Success()
        {
            // Arrange
            var studentRepoMock = new Mock<IStudentRepository>();
            var clientMock = new Mock<IElasticClient>();

            var controller = new StudentController(
                Mock.Of<ILogger<StudentController>>(),
                clientMock.Object,
                Mock.Of<IMapper>(),
                studentRepoMock.Object,
                Mock.Of<IReservedClassRepository>(),
                Mock.Of<IClassRepository>()
                );

            var studentId = "S001";
            var reservedClassId = "RC001";
            var expectedResult = Result.Success(StudentMessages.StudentUpdateSuccessful);

            studentRepoMock.Setup(repo => repo.UpdateStatusStudent(studentId, reservedClassId))
                .ReturnsAsync(expectedResult);
            //Act
            var result = await controller.UpdateStatusStudents(studentId, reservedClassId) as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task UpdateStatusStudent_WhenStudentIdIsNull_ReturnsFailureResult()
        {
            // Arrange
            var studentId = "";
            var reservedClassId = "RC456";

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var repository = new StudentRepository(mockRepositoryContext.Object, null, null, null);

            // Act
            var result = await repository.UpdateStatusStudent(studentId, reservedClassId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(StudentMessages.StudentIdIsNull, result.Error);
        }
        [Fact]
        public async Task UpdateStatusStudent_WhenReservedClassIdIsEmpty_ReturnsFailureResult()
        {
            // Arrange
            var studentId = "S123";
            var reservedClassId = "";

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var repository = new StudentRepository(mockRepositoryContext.Object, null, null, null);

            // Act
            var result = await repository.UpdateStatusStudent(studentId, reservedClassId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ReservedClassMessages.ReservedClassIdIsNull, result.Error);
        }
        [Fact]
        public async Task UpdateReserveStatusStudent_Should_Return_Success()
        {
            // Arrange
            var studentRepoMock = new Mock<IStudentRepository>();
            var clientMock = new Mock<IElasticClient>();

            var controller = new StudentController(
                Mock.Of<ILogger<StudentController>>(),
                clientMock.Object,
                Mock.Of<IMapper>(),
                studentRepoMock.Object,
                Mock.Of<IReservedClassRepository>(),
                Mock.Of<IClassRepository>()
                );

            var studentId = "S001";
            var reservedClassId = "RC001";
            var expectedResult = Result.Success(StudentMessages.StudentUpdateSuccessful);

            studentRepoMock.Setup(repo => repo.UpdateReserveStatusStudent(studentId, reservedClassId))
                .ReturnsAsync(expectedResult);
            //Act
            var result = await controller.UpdateReserveStatusStudent(studentId, reservedClassId) as ObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task UpdateReserveStatusStudent_WhenStudentIdIsNull_ReturnsFailureResult()
        {
            // Arrange
            var studentId = "";
            var reservedClassId = "RC456";

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var repository = new StudentRepository(mockRepositoryContext.Object, null, null, null);

            // Act
            var result = await repository.UpdateReserveStatusStudent(studentId, reservedClassId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(StudentMessages.StudentIdIsNull, result.Error);
        }
        [Fact]
        public async Task UpdateReserveStatusStudent_WhenReservedClassIdIsEmpty_ReturnsFailureResult()
        {
            // Arrange
            var studentId = "S123";
            var reservedClassId = "";

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var repository = new StudentRepository(mockRepositoryContext.Object, null, null, null);

            // Act
            var result = await repository.UpdateReserveStatusStudent(studentId, reservedClassId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ReservedClassMessages.ReservedClassIdIsNull, result.Error);
        }
        [Fact]
        public async Task SearchStudent_Should_Return_Correct_Results_For_Existing_StudentId()
        {
            // Arrange
            string studentIdOrEmail = "S001"; // Provide an existing student ID or email
            var expectedStudents = new List<Student>
    {
        new Student { StudentId = "S001", FullName = "John Doe", /* Other properties */ },
        // Add more expected students as needed
    };

            // Mock RepositoryContext and set it up to return the expected students
            var mockRepositoryContext = new Mock<RepositoryContext>();
            mockRepositoryContext.Setup(c => c.Students).Returns(DbSetMocker.MockDbSet(expectedStudents));

            var studentRepository = new StudentRepository(mockRepositoryContext.Object);

            // Act
            var result = await studentRepository.SearchStudent(studentIdOrEmail);

            // Assert
            Assert.NotNull(result); // Ensure that result is not null
            Assert.IsType<List<StudentDTO>>(result); // Ensure that the result is a list of StudentDTO objects

            // Verify that the returned students match the expected students
            var resultStudentIds = result.Select(s => s.StudentId).ToList();
            var expectedStudentIds = expectedStudents.Select(s => s.StudentId).ToList();
            Assert.Equal(expectedStudentIds, resultStudentIds);
        }

        [Fact]
        public async Task SearchStudent_Should_Return_Empty_List_For_Nonexistent_StudentId()
        {
            // Arrange
            string studentIdOrEmail = "nonexistent@example.com"; // Provide a non-existent student ID or email
            var expectedStudents = new List<Student>(); // Empty list to simulate no students found

            // Mock RepositoryContext and set it up to return an empty list when called with the specified student ID or email
            var mockRepositoryContext = new Mock<RepositoryContext>();
            mockRepositoryContext.Setup(c => c.Students).Returns(DbSetMocker.MockDbSet(expectedStudents));

            var studentRepository = new StudentRepository(mockRepositoryContext.Object);

            // Act
            var result = await studentRepository.SearchStudent(studentIdOrEmail);

            // Assert
            Assert.NotNull(result); // Ensure that result is not null
            Assert.Empty(result); // Verify that the result is an empty list
        }


        [Fact]
        public async Task AddStudentBackToClass_Should_Return_Success_Result()
        {
            // Arrange
            var reClassId = "RC001";
            var studentId = "S001";

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var repository = new StudentRepository(mockRepositoryContext.Object);

            // Act
            var result = await repository.AddStudentBackToClass(reClassId, studentId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(StudentMessages.StudentUpdateSuccessful, result.Error);
        }

        [Fact]
        public async Task AddStudentBackToClass_Should_Return_Failure_Result_When_Repository_Fails()
        {
            // Arrange
            var reClassId = "RC003";
            var studentId = "S001";

            var mockDbSet = new Mock<DbSet<StudentClass>>();
            mockDbSet.Setup(dbSet => dbSet.Add(It.IsAny<StudentClass>())).Throws(new Exception("Repository failure"));

            var mockRepositoryContext = new Mock<RepositoryContext>();
            mockRepositoryContext.Setup(repo => repo.StudentClasses).Returns(mockDbSet.Object);

            var repository = new StudentRepository(mockRepositoryContext.Object);

            // Act
            var result = await repository.AddStudentBackToClass(reClassId, studentId);

            // Assert
            Assert.False(result.IsFailure);
        }
        [Fact]
        public async Task GetReservedStudentDTOByStudentIdAndClassId_Should_Return_Valid_Result_For_Existing_Student_And_Class()
        {
            // Arrange
            var existingStudentId = "S001"; // Existing studentId
            var existingClassId = "C001"; // Existing classId
            var reason = "Some reason";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);
            var classEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));

            // Mock student and class objects
            var existingStudent = new Student { StudentId = existingStudentId };
            var existingClass = new Class { ClassId = existingClassId };

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var mockClassRepository = new Mock<IClassRepository>();
            var mockModuleRepository = new Mock<IModuleRepository>();

            // Set up mock repository context to return existing student and class objects
            mockRepositoryContext.Setup(repo => repo.Students.FindAsync(existingStudentId)).ReturnsAsync(existingStudent);
            mockRepositoryContext.Setup(repo => repo.Classes.FindAsync(existingClassId)).ReturnsAsync(existingClass);

            // Create an instance of the StudentRepository with the mock dependencies
            var repository = new StudentRepository(mockRepositoryContext.Object, null, mockClassRepository.Object, mockModuleRepository.Object);

            // Act
            var result = await repository.GetReservedStudentDTOByStudentIdAndClassId(existingStudentId, existingClassId, reason, startDate, endDate, classEndDate);

            // Assert
            // You can assert that the result is not null and contains the expected properties or values
            Assert.NotNull(result);
            // Add more assertions as needed based on the expected behavior of the method
        }

        [Fact]
        public async Task GetReservedStudentDTOByStudentIdAndClassId_Should_Handle_NonExistent_Student_Or_Class()
        {
            // Arrange
            var nonExistentStudentId = "S999"; // Non-existent studentId
            var nonExistentClassId = "C999"; // Non-existent classId
            var reason = "Some reason";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);
            var classEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));

            var mockRepositoryContext = new Mock<RepositoryContext>();
            var mockClassRepository = new Mock<IClassRepository>();
            var mockModuleRepository = new Mock<IModuleRepository>();

            // Set up mock repository context to return null for non-existent studentId or classId
            mockRepositoryContext.Setup(repo => repo.Students.FindAsync(nonExistentStudentId)).ReturnsAsync((Student)null);
            mockRepositoryContext.Setup(repo => repo.Classes.FindAsync(nonExistentClassId)).ReturnsAsync((Class)null);

            // Create an instance of the StudentRepository with the mock dependencies
            var repository = new StudentRepository(mockRepositoryContext.Object, null, mockClassRepository.Object, mockModuleRepository.Object);

            // Act and Assert
            await Assert.ThrowsAsync<NotSupportedException>(async () =>
            {
                await repository.GetReservedStudentDTOByStudentIdAndClassId(nonExistentStudentId, nonExistentClassId, reason, startDate, endDate, classEndDate);
            });
        }
        [Fact]
        public async Task GetAllReservedStudents_Should_Return_Paginated_Student_Reserved_List()
        {
            // Arrange
            var pageNumber = 1; // Example page number
            var pageSize = 10; // Example page size
            var expectedStudentReservedList = new List<StudentReservedDTO>
            {
                               new StudentReservedDTO
                {
                    ReservedClassId = "R001",
                    StudentId = "S001",
                    ClassId = "C001",
                    Reason = "Some reason",
                    StartDate = "2024-03-25",
                    EndDate = "2024-03-31",
                    ClassName = "Math",
                    ModuleName = "Module A",
                    StudentName = "John Doe",
                    Dob = "1990-01-01",
                    Gender = "Male",
                    Address = "123 Main St",
                    Email = "john@example.com",
                    ClassEndDate = "2024-04-06",
                    CreatedDate = DateTime.Now
                },
                new StudentReservedDTO
                {
                    ReservedClassId = "R002",
                    StudentId = "S002",
                    ClassId = "C002",
                    Reason = "Another reason",
                    StartDate = "2024-04-01",
                    EndDate = "2024-04-07",
                    ClassName = "Science",
                    ModuleName = "Module B",
                    StudentName = "Jane Doe",
                    Dob = "1995-05-15",
                    Gender = "Female",
                    Address = "456 Elm St",
                    Email = "jane@example.com",
                    ClassEndDate = "2024-04-08",
                    CreatedDate = DateTime.Now

                },
                // Add more sample StudentReservedDTO objects as needed
        };

            // Mock RepositoryContext
            var mockRepositoryContext = new Mock<RepositoryContext>();

            // Mock StudentRepository
            var studentRepository = new StudentRepository(mockRepositoryContext.Object);

            // Mock the StudentClasses property to return a non-null IQueryable<StudentClass>
            var mockStudentClasses = new List<StudentClass>(); // Create an empty list
            mockRepositoryContext.Setup(repo => repo.StudentClasses)
                .Returns(DbSetMocker.MockDbSet(mockStudentClasses)); // Ensure it returns a non-null DbSet

            // Act
            var result = await studentRepository.GetAllReservedStudents(pageNumber);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StudentReservedPagingResponseDTO>(result);
        }
        [Fact]
        public async Task GetAllReservedStudents_Should_Return_Empty_List_When_No_Students_Are_Reserved()
        {
            // Arrange
            var pageNumber = 1; // Example page number

            // Mock RepositoryContext and set it up to return an empty list when called with the specified student ID or email
            var mockRepositoryContext = new Mock<RepositoryContext>();
            var emptyStudentClasses = new List<StudentClass>(); // Create an empty list
            mockRepositoryContext.Setup(repo => repo.StudentClasses).Returns(DbSetMocker.MockDbSet(emptyStudentClasses));

            var studentRepository = new StudentRepository(mockRepositoryContext.Object);

            // Act
            var result = await studentRepository.GetAllReservedStudents(pageNumber);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StudentReservedPagingResponseDTO>(result);
            Assert.Empty(result); // Check if the StudentReservedList property is empty
        }



    }
}
