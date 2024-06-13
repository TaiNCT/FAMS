using SyllabusManagementAPI.Entities.DTO;
using Entities.DTO.SyllabusDay;

namespace SyllabusManagementAPI.ServiceContracts
{
    public interface ISyllabusDayService
    {
        Task<ResponseDTO> GetSyllabusDaysOutlineBySyllabusIdAsync(string syllabusId);
		Task<SyllabusDayDTO> CreateSyllabusDayAsync(SyllabusDayForCreationDTO syllabusDay, string syllabusId);
		Task ImportSyllabusDay(string syllabusId, IFormFile file);
	}
}