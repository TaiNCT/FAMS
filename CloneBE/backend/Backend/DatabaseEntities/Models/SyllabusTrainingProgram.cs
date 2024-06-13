using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[PrimaryKey("SyllabusId", "TrainingProgramCode")]
[Table("SyllabusTrainingProgram")]
public partial class SyllabusTrainingProgram
{
    [Key]
    public int SyllabusId { get; set; }

    [Key]
    public int TrainingProgramCode { get; set; }

    [ForeignKey("SyllabusId")]
    [InverseProperty("SyllabusTrainingProgram")]
    public virtual Syllabus Syllabus { get; set; }

    [ForeignKey("TrainingProgramCode")]
    [InverseProperty("SyllabusTrainingProgram")]
    public virtual TrainingProgram TrainingProgram  { get; set; }
}
