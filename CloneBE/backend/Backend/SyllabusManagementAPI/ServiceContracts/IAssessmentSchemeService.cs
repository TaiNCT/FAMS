using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.AssessmentScheme;
using Entities.Models;

namespace SyllabusManagementAPI.ServiceContracts
{
    public interface IAssessmentSchemeService
    {
        Task<ResponseDTO> GetAssessmentSchemeByIdAsync(string syllabusId);

        Task<AssessmentSchemeDTO> CreateAssessmentSchemeAsync(string syllabusId, AssessmentSchemeDTO assessmentScheme);

        Task<AssessmentSchemeDTO> ImportAssessmentScheme(string syllabusId, IFormFile file);
	}
}
