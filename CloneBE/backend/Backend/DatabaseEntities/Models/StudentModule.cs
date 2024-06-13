using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("StudentModule")]
[Index("StudentModuleId", Name = "UQ__StudentM__4A54FA662D560FFE", IsUnique = true)]
public partial class StudentModule
{
    [Required]
    [Column("studentModuleId")]
    [StringLength(36)]
    public string StudentModuleId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string StudentId { get; set; }

    [Required]
    [StringLength(36)]
    public string ModuleId { get; set; }

    [Column(TypeName = "double")]
    public double ModuleScore { get; set; }

    public int ModuleLevel { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("StudentModules")]
    public virtual Module Module { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentModules")]
    public virtual Student Student { get; set; }
}
