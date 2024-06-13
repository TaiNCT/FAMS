using ReservationManagementAPI.Entities.DTOs;
using Entities.Models;
using ReservationManagementAPI.Exceptions;

namespace ReservationManagementAPI.Contracts

{
    public interface IStudentRepository : IRepositoryBase<Student>
    {

        Task<List<StudentReservedDTO>> GetAllReservedStudents(int pageNumber);

        Task<Result> UpdateStatusStudent(string studentId, string reservedClassId);
        Task<StudentDTO> GetStudentById(string studentId);
        Task<List<ClassDTO>>? SearchStudent(string studentIdOrEmail);
        Task<List<StudentReservedDTO>> GetAllReservedStudentsByReserveTime();
        Task<StudentReservedDTO> GetReservedStudentDTOByStudentIdAndClassId(string studentId, string classId, string reason, DateTime startDate, DateTime endDate, DateOnly classEndDate);
        Task<Result> UpdateReserveStatusStudent(string studentId, string reservedClassId);
        Task AddStudentBackToClass(string reClassId, string studentId);
    }
}
