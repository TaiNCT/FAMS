using Entities.Models;

namespace StudentInfoManagementAPI.DTO
{
    public class PaginatedStudentListDTOO
    {
        public IEnumerable<StudentDTOO> Students { get; set; }
        public int TotalCount { get; set; }
    }
}
