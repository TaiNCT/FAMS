using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class ClassTrainerLocation
{
    public int Id { get; set; }

    public string ClassId { get; set; } = null!;

    public string SyllabusUnitId { get; set; } = null!;

    public string? TrainerId { get; set; }

    public string? LocationId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual SyllabusUnit SyllabusUnit { get; set; } = null!;

    public virtual User? Trainer { get; set; }
}
