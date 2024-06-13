using Entities.Context;
using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Models;

namespace SyllabusManagementAPI.Repository
{
    public class AssessmentSchemeRepository : RepositoryBase<AssessmentScheme>, IAssessmentSchemeRepository
    {
        private readonly FamsContext _context;

        public AssessmentSchemeRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AssessmentScheme?> GetAssessmentSchemeByIdAsync(string syllabusId)
        {
            return await FindByConditionV2(a => a.SyllabusId == syllabusId).Include(s => s.Syllabus).FirstOrDefaultAsync();
        }

        public void CreateAssessmentSchemeAsync(string syllabusId, AssessmentScheme assessmentScheme)
        {
            assessmentScheme.SyllabusId = syllabusId;
            Create(assessmentScheme);
        }

        public void DeleteAssessmentSchemesAsync(string syllabusId)
        {
            var assessmentSchemes = _context.AssessmentSchemes.Where(x => x.SyllabusId == syllabusId);
            if (assessmentSchemes == null || !assessmentSchemes.Any())
            {
                return;
            }
            _context.AssessmentSchemes.RemoveRange(assessmentSchemes);
        }
    }
}
