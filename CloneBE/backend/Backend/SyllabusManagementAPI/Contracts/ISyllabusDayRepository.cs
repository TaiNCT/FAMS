using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface ISyllabusDayRepository
    {
        Task<SyllabusDay?> GetSyllabusDaysBySyllabusIdAsync(string syllabusId);

        Task<IEnumerable<SyllabusDay>> GetSyllabusDaysOutlineBySyllabusIdAsync(string syllabusId);

        void CreateSyllabusDayAsync(string syllabusId, SyllabusDay syllabusDay);

        void DeleteSyllabusDaysAsync(string SyllabusId);
    }
}
