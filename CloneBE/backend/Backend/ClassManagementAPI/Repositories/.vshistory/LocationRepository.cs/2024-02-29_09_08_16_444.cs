using ClassManagementAPI.Models;
using ClassManagementAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPI.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly FamsContext _context;
        public LocationRepository(FamsContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllLocation()
        {
            return await _context.Locations.ToListAsync();
        }
        public async Task<Location> GetLocationById(string id)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == id);
        }
    }
}
