using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.UserDTO;
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
        Task<User?> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        
        Task DeleteUserFromClassAsync(string userId, string classId);

    }
}
