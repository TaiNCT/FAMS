using System.ComponentModel.DataAnnotations;

namespace TrainingProgramManagementAPI.Payloads.Requests
{
    public class UpdateTrainingProgramRequest
    {
        [Required]
        public string TrainingProgramCode { get; set; } = string.Empty;
        public int Id { get; set; } = 0;

        [Required]
        public string UpdatedBy { get; set; } = string.Empty;

        // Those fields depend on how many syllabus in training program
        public int Days { get; set; } = 0;

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        // List of syllabusID selected
        public List<string> SyllabiIDs { get; set; } = new List<string>();
    }

    public static class UpdateTrainingProgramRequestExtension
    {
        public static TrainingProgramDto ToTrainingProgramDto(this UpdateTrainingProgramRequest reqObj)
        {
            return new TrainingProgramDto()
            {
                TrainingProgramCode = reqObj.TrainingProgramCode!,
                Id = reqObj.Id,
                UpdatedBy = reqObj.UpdatedBy,
                UpdatedDate = DateTime.UtcNow,
                Days = reqObj.Days,
                Name = reqObj.Name,
                Status = reqObj.Status
            };
        }
    }


}
