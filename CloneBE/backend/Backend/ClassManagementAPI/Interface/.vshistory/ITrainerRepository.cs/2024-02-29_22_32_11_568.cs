using ClassManagementAPI.Models;

namespace ClassManagementAPI.Interface
{
    public interface ITrainerRepository
    {
        Task<List<TrainingProgram>> GetAllTraningProgramList();
    }
}
