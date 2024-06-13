using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
    public interface IStudentDetailinClass
    {
        StudentClassDTO GetStudentDetails(string studentId);
    }
}

