using ClassManagementAPI.Models;

namespace ClassManagementAPI.Interface
{
    public interface IClassUserRepository
    {
        Task<List<ClassUser>> GetClassUsersByClassID(string id, string userType = null);
    }
}
