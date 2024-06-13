namespace TrainingProgramManagementAPI.DTOs
{
    public class TechnicalCodeDto
    {
        public string TechnicalCodeId { get; set; } = null!;

        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public string TechnicalCodeName { get; set; } = null!;

        public virtual ICollection<TrainingProgramDto> TrainingPrograms { get; set; } = new List<TrainingProgramDto>();
    }
}