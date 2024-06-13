using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Interfaces;
using System.Runtime.CompilerServices;

namespace ScoreManagementAPI.Repository
{
    public interface ICertRepository
    {
        public Task<bool> UpdateStudent(FormatJSON data);
        public Task<StudentCertDTO> GetStudentCert(string id, string classid);
        public Task<object> GetMajor();
        public Task<int> UpdateOtherInfoStudent(FormatJSONOthers data);


    }
}
