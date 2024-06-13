using SyllabusManagementAPI.Contracts;
using Entities.Context;
using Entities.Models;

namespace SyllabusManagementAPI.Repository
{
    public class UnitChapterRepository : RepositoryBase<UnitChapter>, IUnitChapterRepository
    {
        private readonly FamsContext _context;

        public UnitChapterRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public void CreateUnitChapterAsync(string syllabusUnitId, UnitChapter unitChapter)
        {
            unitChapter.SyllabusUnitId = syllabusUnitId;
            Create(unitChapter);
        }

    }
}