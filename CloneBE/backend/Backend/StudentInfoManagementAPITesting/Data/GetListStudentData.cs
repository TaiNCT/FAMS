using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentInfoManagementAPI.DTO;
using Entities.Models;

namespace StudentInfoManagementAPITesting.Data
{
    public class GetListStudentData
    {
        public List<StudentDTO> studentDTOs()
        {

            return new List<StudentDTO>{
                new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            StudentId = "S123",
            Id = 1,
            FullName = "John Doe",
            Dob = new DateTime(1990, 1, 1),
            Gender = "Male",
            Phone = "1234567890",
            Email = "john.doe@example.com",
            MajorId = "M001",
            GraduatedDate = new DateTime(2012, 5, 15),
            Gpa = 3.5m,
            Address = "123 Main St, City",
            Faaccount = "FA123",
            Type = 1,
            Status = "Active",
            JoinedDate = new DateTime(2010, 9, 1),
            Area = "Urban",
            Recer = "Jane Smith",
            University = "ABC University"
        }
    },
    new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            StudentId = "S124",
            Id = 2,
            FullName = "Jane Smith",
            Dob = new DateTime(1992, 3, 15),
            Gender = "Female",
            Phone = "9876543210",
            Email = "jane.smith@example.com",
            MajorId = "M002",
            GraduatedDate = new DateTime(2014, 6, 30),
            Gpa = 3.8m,
            Address = "456 Elm St, Town",
            Faaccount = "FA124",
            Type = 1,
            Status = "Active",
            JoinedDate = new DateTime(2011, 8, 1),
            Area = "Suburban",
            Recer = "John Doe",
            University = "XYZ University"
        }
    },
    new StudentDTO
    {
        StudentInfoDTO = new StudentInfoDTO
        {
            StudentId = "S125",
            Id = 3,
            FullName = "Michael Johnson",
            Dob = new DateTime(1988, 8, 20),
            Gender = "Male",
            Phone = "5556667777",
            Email = "michael.johnson@example.com",
            MajorId = "M003",
            GraduatedDate = new DateTime(2010, 4, 20),
            Gpa = 3.2m,
            Address = "789 Oak St, Village",
            Faaccount = "FA125",
            Type = 1,
            Status = "Inactive",
            JoinedDate = new DateTime(2009, 7, 1),
            Area = "Rural",
            Recer = "Emily Brown",
            University = "PQR University"
        }
    }
            };
        }
    }
}


