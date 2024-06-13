using Entities.Models;
using ClassManagementAPI.Interface;
using Microsoft.EntityFrameworkCore;
using Entities.Context;

namespace ClassManagementAPI.Repositories
{
    public class FSURepository : IFSURepository
    {
        private readonly FamsContext _context;
        public FSURepository(FamsContext context)
        {
            _context = context;
        }

        public async Task<List<Fsu>> GetAllFSU()
        {
            return await _context.Fsus.ToListAsync();
        }

    }
}
