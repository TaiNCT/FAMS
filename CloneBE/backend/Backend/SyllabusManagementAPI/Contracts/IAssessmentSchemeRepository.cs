using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface IAssessmentSchemeRepository
    {
        Task<AssessmentScheme?> GetAssessmentSchemeByIdAsync(string syllabusId);

        void CreateAssessmentSchemeAsync(string syllabusId, AssessmentScheme assessmentScheme);

        void DeleteAssessmentSchemesAsync(string SyllabusId);

    }
}
