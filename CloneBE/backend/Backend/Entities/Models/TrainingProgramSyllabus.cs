using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TrainingProgramSyllabus
{
    public int Id { get; set; }

    public string SyllabusId { get; set; } = null!;

    public string TrainingProgramCode { get; set; } = null!;

    public virtual Syllabus Syllabus { get; set; } = null!;

    public virtual TrainingProgram TrainingProgramCodeNavigation { get; set; } = null!;
}
