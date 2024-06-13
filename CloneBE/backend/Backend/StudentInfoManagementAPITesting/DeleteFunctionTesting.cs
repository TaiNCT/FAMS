// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AutoMapper;
// using Entities.Context;
// using Entities.Models;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using StudentInfoManagementAPI.Service;
// using StudentInfoManagementAPITesting.Data;
//
// namespace StudentInfoManagementAPITesting
// {
//     public class DeleteFunctionTesting
//     {
//         private readonly Mock<FamsContext> _dbContextMock;
//         private readonly Mock<IMapper> _mapperMock;
//         private readonly Mock<IStudentService_QuyNDC> _studentMock;
//
//         public DeleteFunctionTesting()
//         {
//             _dbContextMock = new Mock<FamsContext>();
//             _mapperMock = new Mock<IMapper>();
//             _studentMock = new Mock<IStudentService_QuyNDC>();
//         }
//
//         [Fact]
//         public void DeleteStudent_Success()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<FamsContext>()
//                 .UseInMemoryDatabase(databaseName: "TestDatabase")
//                 .Options;
//
//             using (var context = new FamsContext(options))
//             {
//                 var studentId = "testId";
//                 var student = new Student
//                 {
//                     StudentId = studentId,
//                     FullName = "John Doe",
//                     Dob = new DateTime(1990, 1, 1),
//                     Gender = "Male",
//                     Email = "john@example.com",
//                     MutatableStudentId = "",
//                     MajorId = "1",
//                     GraduatedDate = new DateTime(2015, 5, 1),
//                     Gpa = 3.5m,
//                     Address = "123 Main St",
//                     Faaccount = "1234567890",
//                     Type = 1,
//                     Status = "Inactive",
//                     JoinedDate = DateTime.Now,
//                     Area = "Science",
//                     University = "Example University",
//                     Scores = Enumerable.Empty<Score>().ToList()
//                 };
//                 context.Students.Add(student);
//                 context.SaveChanges();
//
//                 var service = new StudentService_QuyNDC(context, null, null);
//
//                 // Act
//                 var result = service.DeleteStudent(studentId);
//
//                 // Assert
//                 Assert.True(result.IsSuccess);
//                 Assert.Equal("Disabled", student.Status);
//             }
//         }
//
//         [Fact]
//         public void DeleteStudent_AttemptToDeleteActiveStudent()
//         {
//             var options = new DbContextOptionsBuilder<FamsContext>()
//                 .UseInMemoryDatabase(databaseName: "TestData")
//                 .Options;
//
//             using (var context = new FamsContext(options))
//             {
//                 var studentId = "testId";
//                 var student = new Student
//                 {
//                     StudentId = studentId,
//                     FullName = "John Doe",
//                     Dob = new DateTime(1990, 1, 1),
//                     Gender = "Male",
//                     Email = "john@example.com",
//                     MajorId = "1",
//                     MutatableStudentId = "",
//                     GraduatedDate = new DateTime(2015, 5, 1),
//                     Gpa = 3.5m,
//                     Address = "123 Main St",
//                     Faaccount = "1234567890",
//                     Type = 1,
//                     Status = "Active",
//                     JoinedDate = DateTime.Now,
//                     Area = "Science",
//                     University = "Example University",
//                     Scores = Enumerable.Empty<Score>().ToList()
//                 };
//                 context.Students.Add(student);
//                 context.SaveChanges();
//
//                 var service = new StudentService_QuyNDC(context, null, null);
//
//                 var result = service.DeleteStudent(studentId);
//
//                 Assert.True(result.IsSuccess);
//                 Assert.Equal("Cannot change status", result.Result);
//                 Assert.Equal("active", student.Status.ToLower());
//             }
//         }
//
//         [Fact]
//         public void DeleteStudentInBatch_Success()
//         {
//             // Arrange
//             var dbContext = new DeleteInBatch();
//             var testData = dbContext.Students;
//
//             var mockDbSet = new Mock<DbSet<Student>>();
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(testData.AsQueryable().GetEnumerator());
//             _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);
//
//             var service = new StudentService_QuyNDC(_dbContextMock.Object, null, null);
//
//             // Act
//             var response = service.DeleteStudentInBatch("id1,id2,id3");
//
//             // Assert
//             Assert.True(response.IsSuccess);
//             Assert.Empty(response.Message);
//
//             // Assert that students with given IDs are disabled
//             foreach (var id in new[] { "id1", "id2", "id3" })
//             {
//                 var student = dbContext.Find(id);
//                 Assert.NotNull(student);
//                 Assert.Equal("Disabled", student.Status);
//             }
//         }
//
//         [Fact]
//         public void DeleteStudentInBatch_NoMatchingStudents()
//         {
//             // Arrange
//             var dbContext = new DeleteInBatch();
//             var testData = dbContext.Students;
//
//             var mockDbSet = new Mock<DbSet<Student>>();
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
//             mockDbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(testData.AsQueryable().GetEnumerator());
//             _dbContextMock.Setup(m => m.Students).Returns(mockDbSet.Object);
//
//             var service = new StudentService_QuyNDC(_dbContextMock.Object, null, null);
//
//             // Act
//             var response = service.DeleteStudentInBatch("non_existent_id");
//
//             // Assert
//             Assert.True(response.IsSuccess);
//             Assert.Empty(response.Message);
//             // No students should be affected
//             foreach (var student in dbContext.Students)
//             {
//                 Assert.NotEqual("Disabled", student.Status);
//             }
//         }
//
//
//
//
//
//
//
//
//         private static DbSet<T> MockDbSet<T>(IEnumerable<T> elements) where T : class
//         {
//             var queryable = elements.AsQueryable();
//             var dbSetMock = new Mock<DbSet<T>>();
//             dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
//             dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
//             dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
//             dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
//             return dbSetMock.Object;
//         }
//     }
//
//
// }
//
//
