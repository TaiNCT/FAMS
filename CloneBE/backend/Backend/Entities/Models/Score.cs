using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Score
{
    public int Id { get; set; }

    public string ScoreId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string AssignmentId { get; set; } = null!;

    public decimal? Score1 { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
