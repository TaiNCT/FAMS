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
        public async Task<UserDTO?> GetUserDTOByIdAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return null;

            return new UserDTO
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                RoleId = user.RoleId
                // Map other properties as needed
            };
        }

        public async Task UpdateUserAsync(UpdateUserDTO updateUser)
        {
            var user = await _context.Users.FindAsync(updateUser.UserId);
            if (user == null)
                throw new ArgumentException("User not found");

            user.RoleId = updateUser.RoleId;
            // Update other properties as needed

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
    }
}
   