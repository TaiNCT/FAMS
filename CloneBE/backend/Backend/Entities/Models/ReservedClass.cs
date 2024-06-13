using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class ReservedClass
{
    public int Id { get; set; }

    public string ReservedClassId { get; set; } = null!;

    public string? StudentId { get; set; }

    public string ClassId { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Reason { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
