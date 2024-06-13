using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface IOutputStandardRepository
    {
        void CreateOutputStandardAsync(OutputStandard outputStandard);
        Task<List<OutputStandard?>> GetAllOutputStandardAsync();
        Task<OutputStandard?> GetOutputStandardByIdAsync(string outputStandardId);
        Task<OutputStandard?> GetOutputStandardByIdAsyncV2(string outputStandardId);
    }
}