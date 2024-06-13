using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class StudentModule
{
    public string StudentModuleId { get; set; }

    public int Id { get; set; }

    public string StudentId { get; set; }

    public string ModuleId { get; set; }

    public decimal ModuleScore { get; set; }

    public int ModuleLevel { get; set; }

    public virtual Module Module { get; set; }

    public virtual Student Student { get; set; }
}
