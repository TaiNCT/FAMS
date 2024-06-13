namespace TrainingProgramManagementAPI.DTOs
{
    public class TechnicalGroupDto
    {
        public string TechnicalGroupId { get; set; } = null!;

        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public string TechnicalGroupName { get; set; } = null!;

        public virtual ICollection<TrainingProgramDto> TrainingPrograms { get; set; } = new List<TrainingProgramDto>();
    }
}