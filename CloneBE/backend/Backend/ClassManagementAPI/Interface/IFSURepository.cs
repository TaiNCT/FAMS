using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface IFSURepository
    {
        Task<List<Fsu>> GetAllFSU();
    }
}
