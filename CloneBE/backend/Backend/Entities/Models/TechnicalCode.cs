using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TechnicalCode
{
    public int Id { get; set; }

    public string TechnicalCodeId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TechnicalCodeName { get; set; } = null!;

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
