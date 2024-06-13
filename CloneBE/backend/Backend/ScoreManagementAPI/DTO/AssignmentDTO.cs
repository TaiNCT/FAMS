namespace ScoreManagementAPI.DTO
{
    public class AssignmentDTO
    {

        public int Id { get; set; }

        public string AssignmentName { get; set; } = null!;

        public string? AssignmentId { get; set; }

        public double? AssignmentScore { get; set; }

        public string? ScoreId { get; set; }


    }
}
