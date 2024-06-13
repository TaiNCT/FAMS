using Entities.Models;

namespace ReservationManagementAPI.Contracts
{
    public interface IClassRepository: IRepositoryBase<Class>
    {
        public Task<Class> GetClassByClassId(string id);
        public Task<List<Class>> GetClasListByModuleList(List<Module> nextModule);
        public Task<bool> CheckStudentAlreadyInClass(string studentId, string classId);
    }
}
