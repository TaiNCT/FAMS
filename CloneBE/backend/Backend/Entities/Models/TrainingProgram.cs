using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TrainingProgram
{
    public int Id { get; set; }

    public string TrainingProgramCode { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Days { get; set; }

    public int? Hours { get; set; }

    public TimeOnly StartTime { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? UserId { get; set; }

    public string? TechnicalCodeId { get; set; }

    public string? TechnicalGroupId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual TechnicalCode? TechnicalCode { get; set; }

    public virtual TechnicalGroup? TechnicalGroup { get; set; }

    public virtual ICollection<TrainingProgramModule> TrainingProgramModules { get; set; } = new List<TrainingProgramModule>();

    public virtual ICollection<TrainingProgramSyllabus> TrainingProgramSyllabi { get; set; } = new List<TrainingProgramSyllabus>();

    public virtual User? User { get; set; }
}
