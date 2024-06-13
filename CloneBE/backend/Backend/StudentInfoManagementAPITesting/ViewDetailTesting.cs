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
using Xunit;
using Entities.Context;

namespace StudentInfoManagementAPITesting
{
    public class ViewDetailTesting
    {
        private readonly Mock<FamsContext> _dbContextMock;
        private readonly IMapper _mapper;

        public ViewDetailTesting()
        {
            _dbContextMock = new Mock<FamsContext>();
            _mapper = ConfigMapper.ConfigureAutoMapper();
        }

        [Fact]
        public void GetStudentDetails_Success()
        {
            // Arrange
            var studentId = "testStudentId";
            var student = new Student
            {
                StudentId = "testStudentId",
                Id = 1,
                FullName = "John Doe",
                Dob = new DateTime(1990, 5, 15),
                Gender = "Male",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                MajorId = "M001",
                GraduatedDate = new DateTime(2015, 6, 30),
                Gpa = 3.5m,
                Address = "123 Main St",
                Faaccount = "FA001",
                Type = 1,
                Status = "Inactive",
                JoinedDate = new DateTime(2012, 9, 1),
                Area = "Computer Science",
                Recer = "Receiver",
                University = "University of Example",
                Audit = 1,
                Mock = 2,
            };

            var classObj = new Class
            {
                ClassId = "testClassId",
                Id = 1,
                ClassStatus = "Active",
                ClassCode = "C001",
                StartDate = new DateOnly(2024, 3, 20),
                EndDate = new DateOnly(2024, 6, 30),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(12, 0),
                ClassName = "Test Class",
                PlannedAttendee = 20,
                AcceptedAttendee = 15,
                ActualAttendee = 10,
            };

            var studentClass = new StudentClass
            {
                StudentClassId = "testStudentClassId",
                Id = 1,
                StudentId = studentId,
                ClassId = "testClassId",
                AttendingStatus = "Attending",
                Result = 90,
                FinalScore = 85.5m,
                Gpalevel = 3,
                CertificationStatus = 1,
                CertificationDate = DateTime.Now,
                Method = 2,
                Student = student,
                Class = classObj
            };

            _dbContextMock.Setup(c => c.StudentClasses)
                .Returns(MockDbSet(new List<StudentClass> { studentClass }));

            StudentClassDTO expectedStudentClassDTO = _mapper.Map<StudentClassDTO>(studentClass);


            var studentDetailService = new StudentDetailinClass(_dbContextMock.Object, _mapper);

            // Act
            StudentClassDTO result = studentDetailService.GetStudentDetails(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StudentClassDTO>(result);
            Assert.Equal(expectedStudentClassDTO.StudentClassId, result.StudentClassId);
            Assert.Equal(expectedStudentClassDTO.Id, result.Id);
            Assert.Equal(expectedStudentClassDTO.StudentId, result.StudentId);
        }


        [Fact]
        public void GetStudentDetails_StudentNotFound()
        {
            var studentId = "nonExistentStudentId";
            _dbContextMock.Setup(c => c.StudentClasses)
                .Returns(MockDbSet(new List<StudentClass>()));

            var studentDetailService = new StudentDetailinClass(_dbContextMock.Object, _mapper);

            // Act
            var result = studentDetailService.GetStudentDetails(studentId);

            // Assert
            Assert.Null(result);
        }



        private static DbSet<T> MockDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSetMock.Object;
        }
    }
}


