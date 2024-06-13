using ClassManagementAPI.Models;

namespace ClassManagementAPI.Interface
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocation();
        Task<Location> GetLocationById(string id);
    }
}
