using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class QuizStudent
{
    public int Id { get; set; }

    public string QuizStudentId { get; set; } = null!;

    public string? StudentId { get; set; }

    public string? QuizId { get; set; }

    public decimal? Score { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual Quiz? Quiz { get; set; }

    public virtual Student? Student { get; set; }
}
