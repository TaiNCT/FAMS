using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class TrainingProgramModule
{
    public string ProgramId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;

    public virtual TrainingProgram Program { get; set; } = null!;
}
