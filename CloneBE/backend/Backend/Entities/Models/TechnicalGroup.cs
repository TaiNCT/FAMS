using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TechnicalGroup
{
    public int Id { get; set; }

    public string TechnicalGroupId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TechnicalGroupName { get; set; } = null!;

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
