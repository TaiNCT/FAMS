using SyllabusManagementAPI.Contracts;
using Entities.Context;
using Entities.Models;

namespace SyllabusManagementAPI.Repository
{
    public class SyllabusUnitRepository : RepositoryBase<SyllabusUnit>, ISyllabusUnitRepository
    {
        private readonly FamsContext _context;

        public SyllabusUnitRepository(FamsContext context) : base(context)
        {
            _context = context;
        }


        public void CreateSyllabusUnitAsync(string syllabusDayId, SyllabusUnit syllabusUnit)
        {
            syllabusUnit.SyllabusDayId = syllabusDayId;
        }

    }
}