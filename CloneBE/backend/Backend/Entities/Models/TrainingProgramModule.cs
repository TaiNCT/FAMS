using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TrainingProgramModule
{
    public string ProgramId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual TrainingProgram Program { get; set; } = null!;
}
