using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Score
{
    public string ScoreId { get; set; }

    public int Id { get; set; }

    public string StudentId { get; set; }

    public string AssignmentId { get; set; }

    public decimal? Score1 { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual Assignment Assignment { get; set; }

    public virtual Student Student { get; set; }
}
