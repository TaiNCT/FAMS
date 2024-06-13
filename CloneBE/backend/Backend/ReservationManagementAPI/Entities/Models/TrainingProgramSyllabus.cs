using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class TrainingProgramSyllabus
{
    public int Id { get; set; }

    public string SyllabusId { get; set; }

    public string TrainingProgramCode { get; set; }

    public virtual Syllabus Syllabus { get; set; }

    public virtual TrainingProgram TrainingProgramCodeNavigation { get; set; }
}
