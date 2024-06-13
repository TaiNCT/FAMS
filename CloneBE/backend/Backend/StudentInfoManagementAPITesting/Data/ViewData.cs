using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace StudentInfoManagementAPITesting.Data
{
    public class ViewData
    {
        public List<Student> GetTestStudents()
        {
            var majors = new List<Major>
    {
        new Major { Id = 1, MajorId = "M001", Name = "Computer Science" },
        new Major { Id = 2, MajorId = "M002", Name = "Physics" },
        new Major { Id = 3, MajorId = "M003", Name = "Biology" }
    };

            return new List<Student>
    {
        new Student
        {
            Id = 1,
            StudentId = "S001",
            FullName = "John Doe",
            Dob = new DateTime(1990, 5, 15),
            Gender = "Male",
            Phone = "123-456-7890",
            Email = "john.doe@example.com",
            MajorId = "M001",
            Major = majors[0], // Associate the major with the student
            GraduatedDate = new DateTime(2015, 6, 30),
            Gpa = 3.5m,
            Address = "123 Main St",
            Faaccount = "@NET_07_Hemsworth",
            Type = 1,
            Status = "Inactive",
            JoinedDate = new DateTime(2012, 9, 1),
            Area = "Computer Science",
            Recer = "Receiver",
            University = "FPT",
            Audit = 1,
            Mock = 2,
            StudentClasses = new List<StudentClass>
            {
                new StudentClass
                {
                    Id = 1,
                    StudentClassId = "SC001",
                    ClassId = "class123",
                    AttendingStatus = "In Class",
                    Result = 80,
                    FinalScore = 90.5m,
                    Gpalevel = 3,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2015, 7, 15),
                    Method = 1
                }
            }
        },
        new Student
        {
            Id = 2,
            StudentId = "S002",
            FullName = "Jane Smith",
            Dob = new DateTime(1992, 8, 20),
            Gender = "Female",
            Phone = "987-654-3210",
            Email = "jane.smith@example.com",
            MajorId = "M002",
            Major = majors[1], // Associate the major with the student
            GraduatedDate = new DateTime(2016, 7, 31),
            Gpa = 3.8m,
            Address = "456 Elm St",
            Faaccount = "FA002",
            Type = 1,
            Status = "Active",
            JoinedDate = new DateTime(2013, 8, 1),
            Area = "Physics",
            Recer = "Receiver",
            University = "UEH",
            Audit = 2,
            Mock = 1,
            StudentClasses = new List<StudentClass>
            {
                new StudentClass
                {
                    Id = 2,
                    StudentClassId = "SC002",
                    ClassId = "C002",
                    AttendingStatus = "In Class",
                    Result = 85,
                    FinalScore = 92.0m,
                    Gpalevel = 4,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2016, 8, 15),
                    Method = 2
                }
            }
        },
        new Student
        {
            Id = 3,
            StudentId = "S003",
            FullName = "David Johnson",
            Dob = new DateTime(1988, 10, 10),
            Gender = "Male",
            Phone = "555-123-4567",
            Email = "david.johnson@example.com",
            MajorId = "M001",
            Major = majors[0], // Associate the major with the student
            GraduatedDate = new DateTime(2014, 5, 31),
            Gpa = 3.2m,
            Address = "789 Oak St",
            Faaccount = "FA003",
            Type = 1,
            Status = "Active",
            JoinedDate = new DateTime(2011, 9, 1),
            Area = "Biology",
            Recer = "Receiver",
            University = "UEL",
            Audit = 1,
            Mock = 3,
            StudentClasses = new List<StudentClass>
            {
                new StudentClass
                {
                    Id = 3,
                    StudentClassId = "SC003",
                    ClassId = "C003",
                    AttendingStatus = "Drop Out",
                    Result = 75,
                    FinalScore = 88.0m,
                    Gpalevel = 3,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2014, 7, 15),
                    Method = 1
                }
            }
        }
    };
        }

        public List<StudentClass> getStudentClass()
        {
            return new List<StudentClass>()
            {
                new StudentClass
                {
                    Id = 1,
                    StudentClassId = "SC001",
                    ClassId = "class123",
                    AttendingStatus = "In Class",
                    Result = 80,
                    FinalScore = 90.5m,
                    Gpalevel = 3,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2015, 7, 15),
                    Method = 1
                },
                new StudentClass
                {
                    Id = 2,
                    StudentClassId = "SC002",
                    ClassId = "C002",
                    AttendingStatus = "In Class",
                    Result = 85,
                    FinalScore = 92.0m,
                    Gpalevel = 4,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2016, 8, 15),
                    Method = 2
                },
                new StudentClass
                {
                    Id = 3,
                    StudentClassId = "SC003",
                    ClassId = "C003",
                    AttendingStatus = "Drop Out",
                    Result = 75,
                    FinalScore = 88.0m,
                    Gpalevel = 3,
                    CertificationStatus = 1,
                    CertificationDate = new DateTime(2014, 7, 15),
                    Method = 1
                }
            };
        }
    }
}


