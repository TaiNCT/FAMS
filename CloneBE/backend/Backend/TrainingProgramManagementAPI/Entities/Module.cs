using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class Module
{
    public string ModuleId { get; set; } = null!;

    public int Id { get; set; }

    public string ModuleName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
}
