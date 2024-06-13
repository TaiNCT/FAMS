namespace StudentInfoManagementAPI.DTO
{
    public class PaginatedStudentListDTO
    {
        public IEnumerable<StudentDTO> Students { get; set; }
        public int TotalCount { get; set; }
    }
}
