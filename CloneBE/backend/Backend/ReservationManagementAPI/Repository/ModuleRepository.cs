using ReservationManagementAPI.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(FamsContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Module> GetModuleByTrainingProgramId(string trainingProgramId)
        {
            var objTrainingProgramModule = await RepositoryContext.TrainingProgramModules
                .Where(c => c.ProgramId == trainingProgramId)
                .FirstOrDefaultAsync();

            var objModule = await RepositoryContext.Modules.Where(c => c.ModuleId == objTrainingProgramModule.ModuleId)
                .FirstOrDefaultAsync();
            return objModule;
        }

        public async Task<List<Module>> GetNextModuleListSuitable(int currentModuleLevel)
        {
            List<StudentModule> studentClasses = await RepositoryContext.StudentModules.Include(m => m.Module).Where(p => p.ModuleLevel == currentModuleLevel+1).ToListAsync();
            List<Module> moduleList = new List<Module>();
            foreach(var i in studentClasses)
            {
                var module = await RepositoryContext.Modules.Where(p => p.ModuleId == i.ModuleId).FirstOrDefaultAsync();
                moduleList.Add(module);
            }
            return moduleList;
        }
    }
}
