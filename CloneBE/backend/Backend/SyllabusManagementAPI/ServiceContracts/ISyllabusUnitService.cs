using SyllabusManagementAPI.Entities.DTO.SyllabusDay;
using SyllabusManagementAPI.Entities.DTO;
using Entities.Models;
using SyllabusManagementAPI.Entities.DTO.SyllabusUnit;

namespace SyllabusManagementAPI.ServiceContracts
{
	public interface ISyllabusUnitService
	{
		Task<SyllabusUnitDTO> CreateSyllabusUnitAsync(SyllabusUnitForCreationDTO syllabusUnit, string syllabusDayId);
	}
}
