namespace TrainingProgramManagementAPI.Payloads.Requests
{
    public class DuplicateTrainingProgramRequest
    {
        public string Code { get; set; } = null!;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
