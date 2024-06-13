using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Keyless]
[Table("TrainingProgramModule")]
[Index("ProgramId", Name = "UQ__Training__75256059DAC2224B", IsUnique = true)]
public partial class TrainingProgramModule
{
    [Required]
    [StringLength(36)]
    public string ProgramId { get; set; }

    [Required]
    [StringLength(36)]
    public string ModuleId { get; set; }

    [ForeignKey("ModuleId")]
    public virtual Module Module { get; set; }

    [ForeignKey("ProgramId")]
    public virtual TrainingProgram Program { get; set; }
}
