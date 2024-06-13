using Microsoft.EntityFrameworkCore;
using ReservationManagementAPI.Contracts;
using Entities.Models;
using Entities.Context;

namespace ReservationManagementAPI.Repository
{
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(FamsContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Assignment> GetAssignmentByAssignmentId(string assignmentId)
        {
            var assignment = await RepositoryContext.Assignments.Where(c => c.AssignmentId == assignmentId).FirstOrDefaultAsync();
            return assignment;
        }
    }
}
