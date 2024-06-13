using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class QuizStudent
{
    public string QuizStudentId { get; set; } = null!;

    public int Id { get; set; }

    public string? StudentId { get; set; }

    public string? QuizId { get; set; }

    public decimal? Score { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual Quiz? Quiz { get; set; }

    public virtual Student? Student { get; set; }
}
