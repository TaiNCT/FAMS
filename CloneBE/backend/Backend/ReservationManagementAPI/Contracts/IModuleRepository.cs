using Entities.Models;

namespace ReservationManagementAPI.Contracts
{
    public interface IModuleRepository : IRepositoryBase<@Module>
    {
        Task<Module> GetModuleByTrainingProgramId(string trainingProgramId);
        Task<List<Module>> GetNextModuleListSuitable(int currentModuleLevel);
    }
}
