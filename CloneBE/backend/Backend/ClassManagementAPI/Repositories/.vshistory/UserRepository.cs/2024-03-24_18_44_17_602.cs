using ClassManagementAPI.Dto.ClassDTO;
using ClassManagementAPI.Dto.UserDTO;
using ClassManagementAPI.Interface;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FamsContext _context;
        public UserRepository(FamsContext context) { _context = context; }
        public async Task<List<User>> GetAllUserByListUserID(List<ClassUser> userID)
        {
            List<User> listUser = new List<User>();
            foreach (ClassUser user in userID)
            {
                var a = await GetUserByUserID(user);
                listUser.Add(a);
            }
            return listUser;
        }
        public async Task<User> GetUserByUserID(ClassUser userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userID.UserId);
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsersByRoleName(string roleName)
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == roleName)
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteUserFromClassAsync(string userId, string classId)
        {
            var classUser = await _context.ClassUsers.FirstOrDefaultAsync(cu => cu.UserId == userId && cu.ClassId == classId);

            if (classUser != null)
            {
                _context.ClassUsers.Remove(classUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found in class.");
            }
        }


    }
}    

   