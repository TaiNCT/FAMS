using ClassManagementAPI.Dto.UserDTO;
using ClassManagementAPI.Interface;
using Entities.Models;
using Entities.Context;
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

        public async Task<ClassUser> AddClassUser(InsertClassUserDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (string.IsNullOrEmpty(dto.ClassId) || string.IsNullOrEmpty(dto.UserId))
            {
                throw new ArgumentException("ClassId and UserId cannot be null or empty.");
            }

            var classUser = new ClassUser
            {
                ClassId = dto.ClassId,
                UserId = dto.UserId,
                UserType = dto.UserType
            };

            await _context.ClassUsers.AddAsync(classUser);
            await _context.SaveChangesAsync();

            return classUser;
        }

        public async Task<List<ClassUser>> GetAllClassUser()
        {
            return await _context.ClassUsers.ToListAsync();
        }
    }
}
