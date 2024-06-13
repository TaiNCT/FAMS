using SyllabusManagementAPI.Entities.DTO;

namespace SyllabusManagementAPI.ServiceContracts
{
	public interface IOutputStandardService
	{
		Task<OutputStandardForCreationDTO> CreateOutputStandardAsync(OutputStandardForCreationDTO outputStandard);
		Task<OutputStandardForCreationDTO> ImportOutputStandard(IFormFile file);
	}
}
