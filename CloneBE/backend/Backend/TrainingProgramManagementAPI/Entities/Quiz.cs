using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class Quiz
{
    public string QuizId { get; set; } = null!;

    public int Id { get; set; }

    public string? ModuleId { get; set; }

    public string? QuizName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Module? Module { get; set; }

    public virtual ICollection<QuizStudent> QuizStudents { get; set; } = new List<QuizStudent>();
}
