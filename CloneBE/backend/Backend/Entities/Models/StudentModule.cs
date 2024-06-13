using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class StudentModule
{
    public int Id { get; set; }

    public string StudentModuleId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public decimal ModuleScore { get; set; }

    public int ModuleLevel { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
