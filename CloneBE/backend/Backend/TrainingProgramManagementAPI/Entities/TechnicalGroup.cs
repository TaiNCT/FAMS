namespace TrainingProgramManagementAPI.Entities;

public partial class TechnicalGroup
{
    public string TechnicalGroupId { get; set; } = null!;

    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string TechnicalGroupName { get; set; } = null!;

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
