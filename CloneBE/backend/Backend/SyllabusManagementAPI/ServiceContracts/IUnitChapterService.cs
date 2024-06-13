using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.UnitChapter;

namespace SyllabusManagementAPI.ServiceContracts
{
	public interface IUnitChapterService
	{
		Task<UnitChapterDTO> CreateSyllabusUnitAsync(UnitChapterForCreationDTO unitChapter, string syllabusUnitId);
	}
}
