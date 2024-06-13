using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class StudentModule
{
    public string StudentModuleId { get; set; } = null!;

    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public decimal ModuleScore { get; set; }

    public int ModuleLevel { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
