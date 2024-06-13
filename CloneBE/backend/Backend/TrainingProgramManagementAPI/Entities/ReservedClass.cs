using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class ReservedClass
{
    public string ReservedClassId { get; set; } = null!;

    public int Id { get; set; }

    public string? StudentId { get; set; }

    public string ClassId { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
