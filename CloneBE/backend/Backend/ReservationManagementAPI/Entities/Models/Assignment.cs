using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Assignment
{
    public string AssignmentId { get; set; }

    public int Id { get; set; }

    public string ModuleId { get; set; }

    public string AssignmentName { get; set; }

    public int AssignmentType { get; set; }

    public DateTime DueDate { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public virtual Module Module { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
