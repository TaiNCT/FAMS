namespace ScoreManagementAPI.DTO
{
    public class QuizStudentDTO
    {
        public string? QuizStudentId { get; set; }
        public string? QuizName { get; set; }
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public string? QuizId { get; set; }
        public double? Score { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }
}
