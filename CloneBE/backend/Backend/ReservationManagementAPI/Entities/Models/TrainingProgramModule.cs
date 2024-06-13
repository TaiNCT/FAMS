using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class TrainingProgramModule
{
    public string ProgramId { get; set; }

    public string ModuleId { get; set; }

    public virtual Module Module { get; set; }

    public virtual TrainingProgram Program { get; set; }
}
