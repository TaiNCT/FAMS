using Entities.Models;
using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
    public interface IAddStudentClassService
    {
        Task<Student> Add(AddStudentDTO student);
    }
}