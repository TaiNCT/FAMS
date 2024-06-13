using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace StudentInfoManagementAPITesting.Data
{
    public class DetailData
    {
         public StudentClass studentClass()
        {
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

            string studentId = "testStudentId";

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
            return studentClass;
        }
    }

    }


