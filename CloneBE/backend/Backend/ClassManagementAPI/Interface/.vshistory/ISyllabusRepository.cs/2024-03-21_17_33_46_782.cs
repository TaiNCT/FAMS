using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.SyllabusDTO;
using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface ISyllabusRepository
    {
        Task<PagedResult<Syllabus>> GetSyllabiByTrainingProgramCode(string trainingProgramCode);
        Task<PagedResult<Syllabus>> GetListSyllabus(string TrainingCode);
        Task<bool> AddProgramSyllabus(TrainingProgramSyllabus insertProgramSyllabus);
        Task<bool> CreateSyllabus(Syllabus syllabus);
        Task DeleteSyllabusCard(string TrainingProgramCode, string SyllabusID);
        Task<IEnumerable<Syllabus>> GetSyllabiByTrainingProgramCode(string trainingProgramCode);
    }
}
