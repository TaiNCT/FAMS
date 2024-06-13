namespace ScoreManagementAPI.DTO
{
    public class ScoreUpdateDTO
    {
        public String StudentId { get; set; }
        public List<QuizUpdateDTO> QuizStudentList { get; set; } = new List<QuizUpdateDTO>();
        public List<AssignmentUpdateDTO> AssignmentStudentList { get; set; } = new List<AssignmentUpdateDTO>();

    }
}
