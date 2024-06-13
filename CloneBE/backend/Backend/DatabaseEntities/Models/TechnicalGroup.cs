using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("TechnicalGroup")]
[Index("TechnicalGroupName", Name = "TechnicalGroupNameUnique", IsUnique = true)]
[Index("TechnicalGroupId", Name = "UQ__Technica__07542F35393FF8C5", IsUnique = true)]
public partial class TechnicalGroup
{
    [Required]
    [Column("technicalGroupId")]
    [StringLength(36)]
    public string TechnicalGroupId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [StringLength(255)]
    public string TechnicalGroupName { get; set; }

    [InverseProperty("TechnicalGroup")]
    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
