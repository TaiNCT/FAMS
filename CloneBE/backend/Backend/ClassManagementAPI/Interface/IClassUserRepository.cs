using ClassManagementAPI.Dto.UserDTO;
using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface IClassUserRepository
    {
        Task<List<ClassUser>> GetClassUsersByClassID(string id, string userType = null);
        Task<ClassUser> AddClassUser(InsertClassUserDTO dto);
        Task<List<ClassUser>> GetAllClassUser();
    }
}
