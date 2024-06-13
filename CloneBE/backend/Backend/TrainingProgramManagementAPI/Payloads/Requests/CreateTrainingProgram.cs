namespace TrainingProgramManagementAPI.Payloads.Requests
{
    public class CreateTrainingProgramRequest
    {
        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; } = null!;


        // Those fields depend on how many syllabus in training program
        public int Days { get; set; } = 0;

        public int Hours { get; set; } = 0;
        // 


        public TimeOnly StartTime { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        // List of syllabusID selected
        public List<string> SyllabiIDs { get; set; } = new List<string>();
    }

    public static class CreateTrainingProgramRequestExtension
    {
        public static TrainingProgramDto ToTrainingProgramDto(this CreateTrainingProgramRequest reqObj)
        {
            return new TrainingProgramDto()
            {
                CreatedBy = reqObj.CreatedBy,
                CreatedDate = reqObj.CreatedDate,
                UpdatedBy = reqObj.UpdatedBy,
                UpdatedDate = reqObj.UpdatedDate,
                Days = reqObj.Days,
                Hours = reqObj.Hours,
                Name = reqObj.Name,
                Status = reqObj.Status
            };
        }
    }
}