using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Assignment
{
    public int Id { get; set; }

    public string AssignmentId { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public string AssignmentName { get; set; } = null!;

    public int AssignmentType { get; set; }

    public DateTime DueDate { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
