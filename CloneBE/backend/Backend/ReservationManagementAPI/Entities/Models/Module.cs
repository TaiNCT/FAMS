using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Module
{
    public string ModuleId { get; set; }

    public int Id { get; set; }

    public string ModuleName { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();

    public virtual ICollection<TrainingProgramModule> TrainingProgramModules { get; set; } = new List<TrainingProgramModule>();
}
