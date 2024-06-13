namespace ScoreManagementAPI.DTO
{
        public class QuizUpdateDTO
        {
                public string QuizStudentId { get; set; }

                public string QuizId { get; set; }

                public double? Score { get; set; }

                public DateTime? SubmissionDate { get; set; }
        }
}
