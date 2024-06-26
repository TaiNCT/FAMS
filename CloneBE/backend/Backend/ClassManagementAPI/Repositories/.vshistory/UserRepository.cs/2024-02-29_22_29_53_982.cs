﻿/*using ClassManagementAPI.Interface;
using ClassManagementAPI.Models;
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
            return await _context.Users.FirstOrDefaultAsync(u=> u.UserId == userID.UserId);
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
*/