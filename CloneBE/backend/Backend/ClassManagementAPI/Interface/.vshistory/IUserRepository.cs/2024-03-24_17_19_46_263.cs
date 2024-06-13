using ClassManagementAPI.Dto.ClassDTO;
using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserByListUserID(List<ClassUser> userID);
        Task<User> GetUserByUserID(ClassUser userID);
        Task<List<User>> GetAll();
        Task<List<Role>> GetAllRole();
        Task<IEnumerable<User>> GetUsersByRoleName(string roleName);
        Task<User> GetUserById(string userId);
        Task<IEnumerable<User>> GetUsersByClassId(string classId);
        Task UpdateUser(User user);
    }
}
