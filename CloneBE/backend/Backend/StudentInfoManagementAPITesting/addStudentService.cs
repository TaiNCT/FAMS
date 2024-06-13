using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using StudentInfoManagementAPI.Service;
using Moq;
using Entities.Models;
using StudentInfoManagementAPI.DTO;
using AutoMapper;
using Nest;
using Xunit;

namespace StudentInfoMangementAPITesting
{
    public class addStudentService
    {
        
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IElasticClient> _elastic;

        public addStudentService()
        {
            _mapperMock = new Mock<IMapper>();
            _elastic = new Mock<IElasticClient>();
        }

        /*[Fact]
        public async Task Add_ValidDTO_ReturnsStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddStudentService0123")
                .Options;

            using (var context = new FamsContext(options))
            {
                var mockDbSet = new Mock<DbSet<Student>>();
                var mockDbSetStudentClass = new Mock<DbSet<StudentClass>>();
                var mockDbSetMajor = new Mock<DbSet<Major>>();
                
                var majors = new List<Major>()
                {
                    new Major()
                    {
                        Id = 1,
                        MajorId = "M001",
                        Students = null
                    }
                };
                await context.Majors.AddRangeAsync(majors);
                await context.SaveChangesAsync();
                
                mockDbSetMajor.As<IAsyncEnumerable<Major>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                    .Returns(new TestAsyncEnumerator<Major>(majors.AsQueryable().GetEnumerator()));
                mockDbSetMajor.As<IQueryable<Major>>().Setup(m => m.Provider)
                    .Returns(new TestAsyncQueryProvider<Major>(majors.AsQueryable().Provider));
                mockDbSetMajor.As<IQueryable<Major>>().Setup(m => m.Expression).Returns(majors.AsQueryable().Expression);
                mockDbSetMajor.As<IQueryable<Major>>().Setup(m => m.ElementType).Returns(majors.AsQueryable().ElementType);
                mockDbSetMajor.As<IQueryable<Major>>().Setup(m => m.GetEnumerator()).Returns(majors.AsQueryable().GetEnumerator());

                

                mockDbSet.Setup(m => m.Add(It.IsAny<Student>()))
                    .Callback((Student student) =>
                    {
                        student.StudentId = Guid.NewGuid().ToString();
                    });

                    mockDbSetStudentClass.Setup(m => m.Add(It.IsAny<StudentClass>()))
                        .Callback((StudentClass studentClass) =>
                        {
                            studentClass.StudentClassId = Guid.NewGuid().ToString();
                        });
                

                var mockContext = new Mock<FamsContext>(options);
                mockContext.Setup(c => c.Students).Returns(mockDbSet.Object);
                mockContext.Setup(c => c.StudentClasses).Returns(mockDbSetStudentClass.Object);
                

                var service = new AddStudentService(mockContext.Object, _elastic.Object, _mapperMock.Object);


                var dto = new AddStudentDTO
                {
                    StudentId = "S123",
                    MutatableStudentId = "MS123",
                    Id = 1,
                    CertificationDate = DateTime.Now,
                    FullName = "John Doe",
                    CertificationStatus = true,
                    Dob = DateTime.Now.AddYears(-25),
                    Gender = "Male",
                    Phone = "1234567890",
                    Email = "john.doe@example.com",
                    MajorId = "M001",
                    GraduatedDate = DateTime.Now.AddYears(-3),
                    Gpa = 3.5m,
                    Address = "123 Main St, City",
                    Faaccount = "FA123",
                    Type = 1,
                    Status = "Active",
                    JoinedDate = DateTime.Now.AddYears(-5),
                    Area = "Urban",
                    Recer = "Jane Smith",
                    University = "ABC University",
                    Audit = 1,
                    Mock = 2,
                    addStudentClassDTOs = new List<AddStudentClassDTO>
                    {
                        new AddStudentClassDTO
                        {
                            StudentClassId = "SC001",
                            Id = 1,
                            StudentId = "S123",
                            ClassId = "C001",
                            AttendingStatus = "Present",
                            Result = 1,
                            FinalScore = 85.5m,
                            Gpalevel = 3,
                            CertificationStatus = 1,
                            CertificationDate = DateTime.Now,
                            Method = 2
                        }
                    }
                };


                // Act
                var student = await service.Add(dto);

                // Assert 
                Assert.NotNull(student);
                Assert.NotNull(student.StudentId);
                Assert.Equal(dto.Email, student.Email);
            }
        } */
        
        [Fact]
        public async Task Add_NullDTO_ReturnsStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddStudentService")
                .Options;

            using (var context = new FamsContext(options))
            {
                var service = new AddStudentService(context, _elastic.Object, _mapperMock.Object);

                var dto = new AddStudentDTO();

                // Assert
                Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Add(dto));
            }
        }
        
        [Fact]
        public async Task Add_ValidDTO_ReturnsStudent()
        {
            // Arrange
            var students = new List<Student>();
            var studentClasses = new List<StudentClass>();
            var majors = new List<Major>
            {
                new Major
                {
                    MajorId = "M001",
                    Students = students
                }
            };

            var mockContext = new Mock<FamsContext>();
            mockContext.Setup(c => c.Students).Returns(MockDbSet(students));
            mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClasses));
            mockContext.Setup(c => c.Majors).Returns(MockDbSet(majors));

            var service = new AddStudentService(mockContext.Object, _elastic.Object, _mapperMock.Object);

            var dto = new AddStudentDTO
            {
                StudentId = "S123",
                MutatableStudentId = "MS123",
                Id = 1,
                CertificationDate = DateTime.Now,
                FullName = "John Doe",
                CertificationStatus = true,
                Dob = DateTime.Now.AddYears(-25),
                Gender = "Male",
                Phone = "1234567890",
                Email = "john.doe@example.com",
                MajorId = "M001",
                GraduatedDate = DateTime.Now.AddYears(-3),
                Gpa = 3.5m,
                Address = "123 Main St, City",
                Faaccount = "FA123",
                Type = 1,
                Status = "Active",
                JoinedDate = DateTime.Now.AddYears(-5),
                Area = "Urban",
                Recer = "Jane Smith",
                University = "ABC University",
                Audit = 1,
                Mock = 2,
                addStudentClassDTOs = new List<AddStudentClassDTO>
                {
                    new AddStudentClassDTO
                    {
                        StudentClassId = "SC001",
                        Id = 1,
                        StudentId = "S123",
                        ClassId = "C001",
                        AttendingStatus = "Present",
                        Result = 1,
                        FinalScore = 85.5m,
                        Gpalevel = 3,
                        CertificationStatus = 1,
                        CertificationDate = DateTime.Now,
                        Method = 2
                    }
                }
            };

            // Act
            var student = await service.Add(dto);

            // Assert 
            Assert.NotNull(student);
            Assert.NotNull(student.StudentId);
            Assert.Equal(dto.Email, student.Email);
        }
        
        private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback((T entity) =>
            {
                // Set unique identifier for StudentId and StudentClassId
                var student = entity as Student;
                if (student != null)
                {
                    student.StudentId = Guid.NewGuid().ToString();
                }

                var studentClass = entity as StudentClass;
                if (studentClass != null)
                {
                    studentClass.StudentClassId = Guid.NewGuid().ToString();
                }

                list.Add(entity);
            });
            return dbSetMock.Object;
        }
        
    }
    
}


