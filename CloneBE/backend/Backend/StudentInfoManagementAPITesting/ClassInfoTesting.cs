using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Nest;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Service;
using Entities.Models;

namespace StudentInfoManagementAPITesting
{
    public class ClassInfoTesting
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly IMapper _mapper;

        private readonly Mock<IElasticClient> _elasticClientMock;

        public ClassInfoTesting()
        {
            _dbContextMock = new Mock<FamsContext>();
            _mapper = ConfigMapper.ConfigureAutoMapper();
            _elasticClientMock = new Mock<IElasticClient>();
        }


        [Fact]
        public async Task AddNewClass_Success()
        {
            // Arrange
            var studentClassDTO = new StudentClassDTO
            {
                StudentClassId = "123",
                Id = 1,
                StudentId = "S123",
                ClassId = "C123",
                AttendingStatus = "Present",
                Result = 90,
                FinalScore = 95.5m,
                Gpalevel = 3,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now,
                Method = 1,
            };

            var mockResponse = new Mock<UpdateByQueryResponse>();
            mockResponse.Setup(r => r.IsValid).Returns(true);

            var mockElasticClient = new Mock<IElasticClient>();
            mockElasticClient
                .Setup(x => x.UpdateByQueryAsync<StudentDTO>(It.IsAny<Func<UpdateByQueryDescriptor<StudentDTO>, IUpdateByQueryRequest>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse.Object);

            // Mock DbContext and DbSet
            var dbContextOptions = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase1")
                .Options;

            var dbContextMock = new Mock<FamsContext>(dbContextOptions);
            var dbSetMock = new Mock<DbSet<StudentClass>>();

            // Setup behavior of DbContext
            dbContextMock.Setup(db => db.StudentClasses).Returns(dbSetMock.Object);

            var service = new StudentService_LamNS(dbContextMock.Object, _mapper, mockElasticClient.Object);

            // Act
            var result = await service.addNewClasInfor(studentClassDTO);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Add Success", result.Result);
        }



        [Fact]
        public async Task AddNewClass_ErrorHandle()
        {
            // Arrange
            var studentClassDTO = new StudentClassDTO
            {
                StudentClassId = "123",
                Id = 1,
                StudentId = "S123",
                ClassId = "C123",
                AttendingStatus = "Present",
                Result = 90,
                FinalScore = 95.5m,
                Gpalevel = 3,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now,
                Method = 1,
            };

            // Mock DbContext and DbSet
            var dbContextOptions = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;

            var dbContextMock = new Mock<FamsContext>(dbContextOptions);
            var dbSetMock = new Mock<DbSet<StudentClass>>();

            // Setup behavior of DbContext
            dbContextMock.Setup(db => db.StudentClasses).Returns(dbSetMock.Object);

            var service = new StudentService_LamNS(null, null, null);

            // Act
            var result = await service.addNewClasInfor(studentClassDTO);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
        }



        [Fact]
        public void getListClassInfor_ReturnsListOfClassInfo_ForInValidStudentId()
        {
            // Arrange
            var studentId = "S00";
            var studentClassData = data().AsQueryable();

            var mockDbSet = new Mock<DbSet<StudentClass>>();
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.Provider).Returns(studentClassData.Provider);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.Expression).Returns(studentClassData.Expression);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.ElementType).Returns(studentClassData.ElementType);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.GetEnumerator()).Returns(studentClassData.GetEnumerator());

            _dbContextMock.Setup(c => c.StudentClasses).Returns(mockDbSet.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<StudentClassDTO>>(It.IsAny<IEnumerable<StudentClass>>()))
                      .Returns((IEnumerable<StudentClass> sc) => sc.Select(s => new StudentClassDTO { Class = new ClassDTO { ClassName = s.Class.ClassName } }));

            var functionUnderTest = new StudentService_LamNS(_dbContextMock.Object, mockMapper.Object, null);

            // Act
            var result = functionUnderTest.getListClassInfor(studentId);

            // Assert
            Assert.True(result.IsSuccess);

            IEnumerable<StudentClassDTO> studentClassDTOs = (IEnumerable<StudentClassDTO>)result.Result;

            List<StudentClassDTO> list = studentClassDTOs.ToList();

            Assert.Equal(0, list.Count);
        }


        [Fact]
        public void getListClassInfor_ReturnsListOfClassInfo_ForValidStudentId()
        {
            // Arrange
            var studentId = "S001";
            var studentClassData = data().AsQueryable();

            var mockDbSet = new Mock<DbSet<StudentClass>>();
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.Provider).Returns(studentClassData.Provider);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.Expression).Returns(studentClassData.Expression);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.ElementType).Returns(studentClassData.ElementType);
            mockDbSet.As<IQueryable<StudentClass>>().Setup(m => m.GetEnumerator()).Returns(studentClassData.GetEnumerator());

            _dbContextMock.Setup(c => c.StudentClasses).Returns(mockDbSet.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<StudentClassDTO>>(It.IsAny<IEnumerable<StudentClass>>()))
                      .Returns((IEnumerable<StudentClass> sc) => sc.Select(s => new StudentClassDTO { Class = new ClassDTO { ClassName = s.Class.ClassName } }));

            var functionUnderTest = new StudentService_LamNS(_dbContextMock.Object, mockMapper.Object, null);

            // Act
            var result = functionUnderTest.getListClassInfor(studentId);

            // Assert
            Assert.True(result.IsSuccess);

            IEnumerable<StudentClassDTO> studentClassDTOs = (IEnumerable<StudentClassDTO>)result.Result;

            List<StudentClassDTO> list = studentClassDTOs.ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("Mathematics", list.ElementAt(0).Class.ClassName);
            Assert.Equal("Physics", list.ElementAt(1).Class.ClassName);
        }


        [Fact]
        public void getListClassInfor_ReturnsErrorResponse_WhenExceptionOccurs()
        {
            // Arrange
            var studentId = "invalidStudentId";

            _dbContextMock.Setup(c => c.StudentClasses).Throws(new Exception("An error occurred"));

            var mockMapper = new Mock<IMapper>();

            var functionUnderTest = new StudentService_LamNS(_dbContextMock.Object, mockMapper.Object, null);

            // Act
            var result = functionUnderTest.getListClassInfor(studentId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred", result.Message);
        }


        [Fact]
        public void GetListClassStudentNotIn_ReturnsCorrectClasses()
        {
            // Arrange
            var studentId = "someStudentId";

            var classesData = new List<Class>
            {
                new Class { Id = 1, ClassId = "class1", ClassCode = "CODE1", ClassName = "Class 1", ClassStatus = "Active", StudentClasses = new List<StudentClass>
                    {new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" } } },
                
                new Class { Id = 2, ClassId = "class2", ClassCode = "CODE2", ClassName = "Class 2", ClassStatus = "Active", StudentClasses = new List<StudentClass> 
                    {new StudentClass { Id = 2, StudentId = "123", ClassId = "class2", AttendingStatus = "Attending", StudentClassId = "studentClassId2" } } },
                
                new Class { Id = 3, ClassId = "class3", ClassCode = "CODE3", ClassName = "Class 3", ClassStatus = "Active", StudentClasses = new List<StudentClass> 
                    {new StudentClass { Id = 3, StudentId = "1234", ClassId = "class3", AttendingStatus = "Attending", StudentClassId = "studentClassId3" } }}
            };

            var studentClassesData = new List<StudentClass>
            {
                new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" }
            };

            var mockContext = new Mock<FamsContext>();
            mockContext.Setup(c => c.Classes).Returns(MockDbSet(classesData));
            mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));

            var classService = new StudentService_LamNS(mockContext.Object, _mapper, _elasticClientMock.Object);

            // Act
            var response = classService.getListClassStudentNotIn(studentId);

            // Assert
            Assert.True(response.IsSuccess);
            var classDTOs = response.Result as IEnumerable<ClassDTO>;
            Assert.NotNull(classDTOs);
            Assert.Equal(2, classDTOs.Count()); // Assuming there are 3 classes and one is already enrolled
            Assert.Contains(classDTOs, c => c.ClassId == "class2");
            Assert.Contains(classDTOs, c => c.ClassId == "class3");
        }

        [Fact]
        public void GetListClassStudentNotIn_ReturnsNoAvailableClasses()
        {
            // Arrange
            var studentId = "someStudentId";

            var classesData = new List<Class>
            {
                new Class { Id = 1, ClassId = "class1", ClassCode = "CODE1", ClassName = "Class 1", ClassStatus = "Active", StudentClasses = new List<StudentClass>
                    {new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" } } },
                new Class { Id = 2, ClassId = "class2", ClassCode = "CODE2", ClassName = "Class 2", ClassStatus = "Active", StudentClasses = new List<StudentClass> 
                    {new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" } } }
            };

            var studentClassesData = new List<StudentClass>
            {
                new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" }
            };

            var mockContext = new Mock<FamsContext>();
            mockContext.Setup(c => c.Classes).Returns(MockDbSet(classesData));
            mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));

            var classService = new StudentService_LamNS(mockContext.Object, _mapper, _elasticClientMock.Object);

            // Act
            var response = classService.getListClassStudentNotIn(studentId);

            // Assert
            Assert.True(response.IsSuccess);
            var classDTOs = response.Result as IEnumerable<ClassDTO>;
            Assert.NotNull(classDTOs);
            Assert.Empty(classDTOs);
        }

        [Fact]
        public void GetListClassStudentNotIn_StudentNotEnrolledInAnyClass()
        {
            // Arrange
            var studentId = "someStudentId";

            var classesData = new List<Class>
            {
                new Class { Id = 1, ClassId = "class1", ClassCode = "CODE1", ClassName = "Class 1", ClassStatus = "Active", StudentClasses = new List<StudentClass>()},
                new Class { Id = 2, ClassId = "class2", ClassCode = "CODE2", ClassName = "Class 2", ClassStatus = "Active", StudentClasses = new List<StudentClass>()},
                new Class { Id = 3, ClassId = "class3", ClassCode = "CODE3", ClassName = "Class 3", ClassStatus = "Active", StudentClasses = new List<StudentClass>()}
            };

            var studentClassesData = new List<StudentClass>
            {
                new StudentClass { Id = 1, StudentId = studentId, ClassId = "class1", AttendingStatus = "Attending", StudentClassId = "studentClassId1" }
            };

            var mockContext = new Mock<FamsContext>();
            mockContext.Setup(c => c.Classes).Returns(MockDbSet(classesData));
            mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));

            var classService = new StudentService_LamNS(mockContext.Object, _mapper, _elasticClientMock.Object);

            // Act
            var response = classService.getListClassStudentNotIn(studentId);

            // Assert
            Assert.True(response.IsSuccess);
            var classDTOs = response.Result as IEnumerable<ClassDTO>;
            Assert.NotNull(classDTOs);
            Assert.Equal(3, classDTOs.Count());
        }

        [Fact]
        public void GetListClassStudentNotIn_ErrorHandle()
        {
            // Arrange


            var classService = new StudentService_LamNS(null, _mapper, _elasticClientMock.Object);

            // Act
            var response = classService.getListClassStudentNotIn("studentId");

            // Assert
            Assert.False(response.IsSuccess);
            var classDTOs = response.Result as IEnumerable<ClassDTO>;
            Assert.Null(classDTOs);

        }
        
        private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSetMock.Object;
        }


        static List<StudentClass> data()
        {
            return new List<StudentClass>
        {
            new StudentClass
            {
                StudentClassId = "1",
                Id = 1,
                StudentId = "S001",
                ClassId = "C001",
                AttendingStatus = "Attending",
                Result = 90,
                FinalScore = 95.5m,
                Gpalevel = 3,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now.AddDays(-10),
                Method = 1,
                Class = new Class
                {
                    ClassId = "C001",
                    Id = 101,
                    ClassName = "Mathematics",
                    ClassStatus = "Active",
                    ClassCode = "MATH101",
                    Duration = 60,
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 3, 1),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(11, 0),
                    AcceptedAttendee = 20,
                    ActualAttendee = 15,
                    FsuId = "FSU001",
                    LocationId = "L001",
                    PlannedAttendee = 25
                }
            },
            new StudentClass
            {
                StudentClassId = "2",
                Id = 2,
                StudentId = "S002",
                ClassId = "C002",
                AttendingStatus = "Attending",
                Result = 85,
                FinalScore = 90.0m,
                Gpalevel = 4,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now.AddDays(-15),
                Method = 2,
                Class = new Class
                {
                    ClassId = "C002",
                    Id = 102,
                    ClassName = "Physics",
                    ClassStatus = "Active",
                    ClassCode = "PHY101",
                    Duration = 60,
                    StartDate = new DateOnly(2024, 1, 5),
                    EndDate = new DateOnly(2024, 3, 5),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(15, 0),
                    AcceptedAttendee = 30,
                    ActualAttendee = 25,
                    FsuId = "FSU002",
                    LocationId = "L002",
                    PlannedAttendee = 35
                }
            },
            new StudentClass
            {
                StudentClassId = "3",
                Id = 3,
                StudentId = "S003",
                ClassId = "C001",
                AttendingStatus = "Attending",
                Result = 88,
                FinalScore = 92.0m,
                Gpalevel = 3,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now.AddDays(-8),
                Method = 1,
                Class = new Class
                {
                    ClassId = "C001",
                    Id = 101,
                    ClassName = "Mathematics",
                    ClassStatus = "Active",
                    ClassCode = "MATH101",
                    Duration = 60,
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 3, 1),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(11, 0),
                    AcceptedAttendee = 20,
                    ActualAttendee = 15,
                    FsuId = "FSU001",
                    LocationId = "L001",
                    PlannedAttendee = 25
                }
            },
            new StudentClass // S001 in class C002
            {
                StudentClassId = "4",
                Id = 4,
                StudentId = "S001",
                ClassId = "C002",
                AttendingStatus = "Attending",
                Result = 95,
                FinalScore = 98.0m,
                Gpalevel = 4,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now.AddDays(-5),
                Method = 1,
                Class = new Class // Class C002
                {
                    ClassId = "C002",
                    Id = 102,
                    ClassName = "Physics",
                    ClassStatus = "Active",
                    ClassCode = "PHY101",
                    Duration = 60,
                    StartDate = new DateOnly(2024, 1, 5),
                    EndDate = new DateOnly(2024, 3, 5),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(15, 0),
                    AcceptedAttendee = 30,
                    ActualAttendee = 25,
                    FsuId = "FSU002",
                    LocationId = "L002",
                    PlannedAttendee = 35
                }
            }
        };
        }

    }
}



