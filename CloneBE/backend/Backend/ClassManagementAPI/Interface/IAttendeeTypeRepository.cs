using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface IAttendeeTypeRepository
    {
        Task<List<AttendeeType>> GetAllAttendeeTypeList();
    }
}
