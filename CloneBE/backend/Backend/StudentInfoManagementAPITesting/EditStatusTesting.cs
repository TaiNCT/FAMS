using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentInfoManagementAPI.DTO;
using Entities.Models;
using StudentInfoManagementAPI.Service;
using StudentInfoManagementAPITesting.Data;
using Entities.Context;

namespace StudentInfoManagementAPITesting
{
    public class EditStatusTesting
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;

        public EditStatusTesting()
        {
            _dbContextMock = new Mock<FamsContext>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task UpdateStudents_NullRequest_Failure()
        {
             // Arrange
         var studentId = "someStudentId";
    
         var reservedClasses = new List<ReservedClass> 
         {
        new ReservedClass
        {
            ReservedClassId = "RC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            //Reason = "Vacation",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 3, 7)
        },
        new ReservedClass
        {
            ReservedClassId = "RC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20)
        },
        new ReservedClass
        {
            ReservedClassId = "RC003",
            Id = 3,
            StudentId = "S004",
            ClassId = "C004",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2025, 4, 20)
        }
    };

         var studentClassesData = new List<StudentClass>
         {
        new StudentClass
        {
            StudentClassId = "SC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "In class",
            Result = 80,
            FinalScore = 90.5m,
            Gpalevel = 3,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2015, 7, 15),
            Method = 1
        },
        new StudentClass
        {
            StudentClassId = "SC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "Drop out",
            Result = 70,
            FinalScore = 85.2m,
            Gpalevel = 2,
            CertificationStatus = 0,
            CertificationDate = null,
            Method = 2
        },
        new StudentClass
        {
            StudentClassId = "SC003",
            Id = 3,
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Reserve",
            Result = 85,
            FinalScore = 95.0m,
            Gpalevel = 4,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2023, 6, 10),
            Method = 1
        }
    };
    
         var mockContext = new Mock<FamsContext>();
         mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));
         mockContext.Setup(c => c.ReservedClasses).Returns(MockDbSet(reservedClasses));
    
         var service = new EditStatusStudentInBatch(mockContext.Object, _mapperMock.Object);

         var request = new List<UpdateStatusRequestDTO>();

         // Act
         var result = service.UpdateBatchStatus(request);

         // Assert
         Assert.False(result.IsSuccess);
         Assert.Equal("Invalid request data.", result.Message);
        
        }

        [Fact]
        public async Task UpdateStudentsDifferentStatus_Failure()
        {
            // Arrange
         var studentId = "someStudentId";
    
         var reservedClasses = new List<ReservedClass> 
         {
        new ReservedClass
        {
            ReservedClassId = "RC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            //Reason = "Vacation",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 3, 7)
        },
        new ReservedClass
        {
            ReservedClassId = "RC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20)
        },
        new ReservedClass
        {
            ReservedClassId = "RC003",
            Id = 3,
            StudentId = "S004",
            ClassId = "C004",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2025, 4, 20)
        }
    };

         var studentClassesData = new List<StudentClass>
         {
        new StudentClass
        {
            StudentClassId = "SC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "In class",
            Result = 80,
            FinalScore = 90.5m,
            Gpalevel = 3,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2015, 7, 15),
            Method = 1
        },
        new StudentClass
        {
            StudentClassId = "SC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "In class",
            Result = 70,
            FinalScore = 85.2m,
            Gpalevel = 2,
            CertificationStatus = 0,
            CertificationDate = null,
            Method = 2
        },
        new StudentClass
        {
            StudentClassId = "SC003",
            Id = 3,
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "In class",
            Result = 85,
            FinalScore = 95.0m,
            Gpalevel = 4,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2023, 6, 10),
            Method = 1
        }
    };
    
         var mockContext = new Mock<FamsContext>();
         mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));
         mockContext.Setup(c => c.ReservedClasses).Returns(MockDbSet(reservedClasses));
    
         var service = new EditStatusStudentInBatch(mockContext.Object, _mapperMock.Object);

         var request = new List<UpdateStatusRequestDTO>
         {
        new UpdateStatusRequestDTO
        {
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "Drop out"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "In Class"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Finish"
        }
    };

         // Act
         var result = service.UpdateBatchStatus(request);

         // Assert
         Assert.False(result.IsSuccess);
         Assert.Equal("Please select students with the same attending status.", result.Message);
         foreach (var studentClass in studentClassesData)
         {
             Assert.Equal("In class", studentClass.AttendingStatus);
         }
        }


        [Fact]
        public async Task UpdateStudentsInClass_To_DropOutStatus_Success()
{
    // Arrange
    var studentId = "someStudentId";
    
    var reservedClasses = new List<ReservedClass>
    {
        new ReservedClass
        {
            ReservedClassId = "RC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            //Reason = "Vacation",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 3, 7)
        },
        new ReservedClass
        {
            ReservedClassId = "RC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20)
        },
        new ReservedClass
        {
            ReservedClassId = "RC003",
            Id = 3,
            StudentId = "S004",
            ClassId = "C004",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2025, 4, 20)
        }
    };

    var studentClassesData = new List<StudentClass>
    {
        new StudentClass
        {
            StudentClassId = "SC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "In class",
            Result = 80,
            FinalScore = 90.5m,
            Gpalevel = 3,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2015, 7, 15),
            Method = 1
        },
        new StudentClass
        {
            StudentClassId = "SC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "Drop out",
            Result = 70,
            FinalScore = 85.2m,
            Gpalevel = 2,
            CertificationStatus = 0,
            CertificationDate = null,
            Method = 2
        },
        new StudentClass
        {
            StudentClassId = "SC003",
            Id = 3,
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Reserve",
            Result = 85,
            FinalScore = 95.0m,
            Gpalevel = 4,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2023, 6, 10),
            Method = 1
        }
    };
    
    var mockContext = new Mock<FamsContext>();
    mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));
    mockContext.Setup(c => c.ReservedClasses).Returns(MockDbSet(reservedClasses));
    
    var service = new EditStatusStudentInBatch(mockContext.Object, _mapperMock.Object);

    var request = new List<UpdateStatusRequestDTO>
    {
        new UpdateStatusRequestDTO
        {
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "Drop out"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "Drop out"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Drop out"
        }
    };

    // Act
    var result = service.UpdateBatchStatus(request);

    // Assert
    Assert.True(result.IsSuccess);
    foreach (var studentClass in studentClassesData)
    {
        Assert.Equal("Drop out", studentClass.AttendingStatus);
    }
}

        [Fact]
        public async Task UpdateStudentsInClass_To_FinishStatus_Failure()
        {
             // Arrange
    var studentId = "someStudentId";
    
    var reservedClasses = new List<ReservedClass>
    {
        new ReservedClass
        {
            ReservedClassId = "RC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            //Reason = "Vacation",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 3, 7)
        },
        new ReservedClass
        {
            ReservedClassId = "RC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2024, 4, 20)
        },
        new ReservedClass
        {
            ReservedClassId = "RC003",
            Id = 3,
            StudentId = "S004",
            ClassId = "C004",
            //Reason = "Personal reasons",
            StartDate = new DateTime(2024, 4, 15),
            EndDate = new DateTime(2025, 4, 20)
        }
    };

    var studentClassesData = new List<StudentClass>
    {
        new StudentClass
        {
            StudentClassId = "SC001",
            Id = 1,
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "In class",
            Result = 80,
            FinalScore = 90.5m,
            Gpalevel = 3,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2015, 7, 15),
            Method = 1
        },
        new StudentClass
        {
            StudentClassId = "SC002",
            Id = 2,
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "Drop out",
            Result = 70,
            FinalScore = 85.2m,
            Gpalevel = 2,
            CertificationStatus = 0,
            CertificationDate = null,
            Method = 2
        },
        new StudentClass
        {
            StudentClassId = "SC003",
            Id = 3,
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Reserve",
            Result = 85,
            FinalScore = 95.0m,
            Gpalevel = 4,
            CertificationStatus = 1,
            CertificationDate = new DateTime(2023, 6, 10),
            Method = 1
        }
    };
    
    var mockContext = new Mock<FamsContext>();
    mockContext.Setup(c => c.StudentClasses).Returns(MockDbSet(studentClassesData));
    mockContext.Setup(c => c.ReservedClasses).Returns(MockDbSet(reservedClasses));
    
    var service = new EditStatusStudentInBatch(mockContext.Object, _mapperMock.Object);

    var request = new List<UpdateStatusRequestDTO>
    {
        new UpdateStatusRequestDTO
        {
            StudentId = "S001",
            ClassId = "C001",
            AttendingStatus = "Finish"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S002",
            ClassId = "C002",
            AttendingStatus = "Finish"
        },
        new UpdateStatusRequestDTO
        {
            StudentId = "S003",
            ClassId = "C003",
            AttendingStatus = "Finish"
        }
    };

    // Act
    var result = service.UpdateBatchStatus(request);

    // Assert
    Assert.True(result.IsSuccess);
    foreach (var studentClass in studentClassesData)
    {
        Assert.Equal("Finish", studentClass.AttendingStatus);
    }

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


    }


}


