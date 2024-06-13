using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Parameters;

namespace SyllabusManagementAPI.ServiceContracts
{
    public interface IElasticService
    {
        Task<ResponseDTO> SearchAsync(string[] keywords, SyllabusParameters syllabusParameters);
    }
}
