namespace ScoreManagementAPI.DTO
{
    public class QuizDTO
    {
        public int Id { get; set; }

        public int? ModuleId { get; set; }
        public string QuizStudentDTO { get; set; }

        public string? QuizName { get; set; }

        public string? QuizId { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public double? QuizScore { get; set; }
    }
}
