namespace ScoreManagementAPI.DTO
{
        public class AssignmentUpdateDTO
        {
                public string StudentId { get; set; }

                public string AssignmentId { get; set; }

                public double? Score1 { get; set; }

                public string ScoreId { get; set; }
                public DateTime? SubmissionDate { get; set; }

        }
}
