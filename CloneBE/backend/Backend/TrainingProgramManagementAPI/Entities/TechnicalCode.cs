using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class TechnicalCode
{
    public string TechnicalCodeId { get; set; } = null!;

    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string TechnicalCodeName { get; set; } = null!;

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
