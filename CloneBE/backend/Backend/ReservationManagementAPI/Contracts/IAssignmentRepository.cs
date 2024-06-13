using Entities.Models;

namespace ReservationManagementAPI.Contracts
{
    public interface IAssignmentRepository : IRepositoryBase<Assignment>
    {
        public Task<Assignment> GetAssignmentByAssignmentId(string assignmentId);
    }
}
