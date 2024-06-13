namespace ReservationManagementAPI.Entities.DTOs
{
    public class AssignmentStudentDTO
    {
        public string AssignmentId { get; set; }
        public string StudentId { get; set; }
        public string AssignmentName { get; set; }
        public decimal? AssignmentScore { get; set; }
    }
}
