using ReservationManagementAPI.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using FluentResults;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(FamsContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Class> GetClassByClassId(string id)
        {
            var item = await RepositoryContext.Classes.FirstOrDefaultAsync(c => c.ClassId == id);
            return item;
        }

        public async Task<List<Class>> GetClasListByModuleList(List<Module> nextModule)
        {
            List<TrainingProgramModule> trainingProgramModuleList = new List<TrainingProgramModule>();
            foreach(var i in nextModule)
            {
                var trainingProgramModules = await RepositoryContext.TrainingProgramModules.Where(p => p.ModuleId == i.ModuleId).ToListAsync();
                trainingProgramModuleList.AddRange(trainingProgramModules);
            }

            List<Class> classList = new List<Class>();
            foreach (var i in trainingProgramModuleList)
            {
                var objClasses = RepositoryContext.Classes.Where(p => p.TrainingProgramCode == i.ProgramId && p.ClassStatus != "Inactive").ToList();
                classList.AddRange(objClasses);
            }
            return classList;
        }

        public async Task<bool> CheckStudentAlreadyInClass (string studentId, string classId)
        {
            bool check = await RepositoryContext.StudentClasses.AnyAsync(sc => sc.StudentId == studentId && sc.ClassId == classId  && 
            sc.AttendingStatus.Equals("InClass"));
            return check;
        }
    }
}
