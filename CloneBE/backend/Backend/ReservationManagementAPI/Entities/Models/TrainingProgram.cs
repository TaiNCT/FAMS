using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class TrainingProgram
{
    public string TrainingProgramCode { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Days { get; set; }

    public int? Hours { get; set; }

    public TimeOnly StartTime { get; set; }

    public string Name { get; set; }

    public string Status { get; set; }

    public string UserId { get; set; }

    public string TechnicalCodeId { get; set; }

    public string TechnicalGroupId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual TechnicalCode TechnicalCode { get; set; }

    public virtual TechnicalGroup TechnicalGroup { get; set; }

    public virtual TrainingProgramModule TrainingProgramModule { get; set; }

    public virtual ICollection<TrainingProgramSyllabus> TrainingProgramSyllabi { get; set; } = new List<TrainingProgramSyllabus>();

    public virtual User User { get; set; }
}
