    using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
    public interface IEditStatusStudentInBatch
    {
        Task<ResponseDTO> EditStudentStatusInBatch(string studentIds, string newStatus, string classId);
    }
}