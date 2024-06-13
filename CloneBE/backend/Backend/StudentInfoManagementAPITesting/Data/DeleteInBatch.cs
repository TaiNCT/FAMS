using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace StudentInfoManagementAPITesting.Data
{
    public class DeleteInBatch: DbContext
    {
        public List<Student> Students { get; set; }

        public DeleteInBatch()
        {
            Students = new List<Student>
            {
                new Student { StudentId = "id1", Status = "Active" },
                new Student { StudentId = "id2", Status = "Active" },
                new Student { StudentId = "id3", Status = "Active" },
            };
        }

        public Student Find(string id)
        {
            return Students.Find(student => student.StudentId == id);
        }
    }
}