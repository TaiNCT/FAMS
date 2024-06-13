using Entities.Context;
using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Models;

namespace SyllabusManagementAPI.Repository
{
    public class SyllabusDayRepository : RepositoryBase<SyllabusDay>, ISyllabusDayRepository
    {
        private readonly FamsContext _context;

        public SyllabusDayRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SyllabusDay?> GetSyllabusDaysBySyllabusIdAsync(string syllabusId)
        {
            return await FindByConditionV2(s => s.SyllabusId.Equals(syllabusId)).FirstOrDefaultAsync();
        }

        public void CreateSyllabusDayAsync(string syllabusId, SyllabusDay syllabusDay)
        {
            syllabusDay.SyllabusId = syllabusId;
            Create(syllabusDay);
        }

        public void DeleteSyllabusDaysAsync(string syllabusId)
        {
            var SyllabusDays = _context.SyllabusDays.Where(x => x.SyllabusId == syllabusId);
            if (SyllabusDays == null || !SyllabusDays.Any())
            {
                return;
            }
            _context.SyllabusDays.RemoveRange(SyllabusDays);
        }

        public async Task<IEnumerable<SyllabusDay>> GetSyllabusDaysOutlineBySyllabusIdAsync(string syllabusId)
        {
            return await _context.SyllabusDays
                .Where(sd => sd.Syllabus.SyllabusId == syllabusId)
                .Include(sd => sd.SyllabusUnits)
                    .ThenInclude(su => su.UnitChapters)
                    .ThenInclude(uc => uc.OutputStandard)
                .Include(sd => sd.SyllabusUnits)
                    .ThenInclude(su => su.UnitChapters)
                    .ThenInclude(uc => uc.DeliveryType)                
                .ToListAsync();
        }
    }
}
