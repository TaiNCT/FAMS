using Entities.Models;

namespace ReservationManagementAPI.Entities.DTOs
{
    public class QuizStudentDTO
    {
        public string QuizStudentId { get; set; }
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string QuizName { get; set; }
        public decimal? QuizScore { get; set;}
        public DateTime? SubmissionDate { get; set; }
    }
}
