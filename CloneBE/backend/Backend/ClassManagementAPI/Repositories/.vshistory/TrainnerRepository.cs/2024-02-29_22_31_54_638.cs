using ClassManagementAPI.Models;
using ClassManagementAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPI.Repositories
{
    public class TrainnerRepository : ITrainerRepository
    {
        private readonly FamsContext _context;

        public TrainnerRepository(FamsContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingProgram>> GetAllTraningProgramList()
        {
            return await _context.TrainingPrograms.ToListAsync();
        }
    }
}
