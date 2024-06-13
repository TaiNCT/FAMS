using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface IUnitChapterRepository
    {
        void CreateUnitChapterAsync(string syllabusUnitId, UnitChapter unitChapter);
    }
}