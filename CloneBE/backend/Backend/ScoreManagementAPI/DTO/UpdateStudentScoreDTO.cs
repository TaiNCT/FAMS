namespace ScoreManagementAPI.DTO
{
    public class UpdateStudentScoreDTO
    {
        public string StudentId { get; set; }

        public double? QuizScore1 { get; set; }
        public double? QuizScore2 { get; set; }
        public double? QuizScore3 { get; set; }
        public double? QuizScore4 { get; set; }
        public double? QuizScore5 { get; set; }
        public double? QuizScore6 { get; set; }
        public double? AssignmentScore1 { get; set; }
        public double? AssignmentScore2 { get; set; }
        public double? AssignmentScore3 { get; set; }


    }
}
