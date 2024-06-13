using Microsoft.AspNetCore.Mvc;
using StudentInfoManagementAPI.DTO;
using System.Data;

namespace StudentInfoManagementAPI.Service
{
    public interface IStudentService_QuyNDC
    {
        public ResponseDTO GetMajor(string id);
        public Task<ResponseDTO> GetListStudent(string? keyword, string? dob, string? gender, string? status, string? sortBy, string? sortOrder, int pageNumber = 1, int pageSize = 5);
        public Task<ResponseDTO> ChangeStudentStatus(string id);
        public Task<ResponseDTO> GetAllId(string? keyword, string? dob, string? gender, string? status, string? sortBy, string? sortOrder, int pageNumber = 1, int pageSize = 5);
        public ResponseDTO GetAllMajor();
        public ResponseDTO GetAllClass();
        public DataTable exportStudentInSystem(string? keyword, string? dob, string? gender, string? status, string? sortBy, string? sortOrder);
    }
}
