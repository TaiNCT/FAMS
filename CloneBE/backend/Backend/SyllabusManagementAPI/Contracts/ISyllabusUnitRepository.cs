using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface ISyllabusUnitRepository
    {
        void CreateSyllabusUnitAsync(string syllabusDayId, SyllabusUnit syllabusUnit);
    }
}