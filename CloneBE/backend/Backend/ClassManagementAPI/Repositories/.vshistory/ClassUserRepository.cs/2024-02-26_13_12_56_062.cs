using ClassManagementAPI.Interface;
using ClassManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPI.Repositories
{
    public class ClassUserRepository : IClassUserRepository
    {
        private readonly FamsContext _context;
        public ClassUserRepository(FamsContext context) { _context = context; }
        public async Task<List<ClassUser>> GetClassUsersByClassID(string classId, string userType = null)
        {
            if(!string.IsNullOrEmpty(userType)) {
                return await _context.ClassUsers.Where(u => u.ClassId == classId && u.UserType == userType).ToListAsync();
            }
            return await _context.ClassUsers.Where(u => u.ClassId == classId).ToListAsync();
        }
    }
}
