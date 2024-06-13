namespace ScoreManagementAPI.DTO
{
    public class AssignmentStudentDTO
    {
        public string? AssignmentStudentId { get; set; }
        public string? AssignmentName { get; set; }

        public int Id { get; set; }

        public string? StudentId { get; set; }


        public string? AssignmentId { get; set; }

        public double? Score { get; set; }

        public DateTime? SubmissionDate { get; set; }
    }
}
