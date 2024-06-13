using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using StudentInfoManagementAPI.DTO;
using System.Data;

namespace StudentInfoManagementAPI.Service
{
    public interface IStudentService_LamNS
    {
        public Task<ResponseDTO> GetListStudentInClass(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy, string? sortOrder = "asc", int pageNumber = 1, int pageSize = 5);

        public ResponseDTO getListClassStudentNotIn(string studentId);

        public Task<ResponseDTO> addNewClasInfor(StudentClassDTO studentClassDTO);

        public ResponseDTO getListClassInfor(string studentId);

        public DataTable exportStudentInclass(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy, string? sortOrder);

        public ResponseDTO SelectAllStudentByClass(string classId);
        Task<String> CreateDocumentAsync();
        Task<bool> DeleteData();

        Task<bool> UpdateStudentClassInforElastic(StudentClass studentClass, Student student);

        public  Task<ResponseDTO> testUpdateStudent(UpdateStudentDTO newStudent);

    }
}
