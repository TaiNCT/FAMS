using Entities.Models;
using ClassManagementAPI.Interface;
using Microsoft.EntityFrameworkCore;
using Nest;
using Entities.Context;

namespace ClassManagementAPI.Repositories
{
    public class AttendeeTypeRepository : IAttendeeTypeRepository
    {
        private readonly FamsContext _context;
        public AttendeeTypeRepository(FamsContext context) { _context = context; }

        public async Task<List<AttendeeType>> GetAllAttendeeTypeList()
        {
            return await _context.AttendeeTypes.ToListAsync();
        }
    }
}
