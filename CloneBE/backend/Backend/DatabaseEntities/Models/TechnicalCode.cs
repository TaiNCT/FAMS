using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("TechnicalCode")]
[Index("TechnicalCodeId", Name = "UQ__Technica__7E6FA295A17DDB44", IsUnique = true)]
[Index("TechnicalCodeName", Name = "technical_code_unique_name", IsUnique = true)]
public partial class TechnicalCode
{
    [Required]
    [Column("technicalCodeId")]
    [StringLength(36)]
    public string TechnicalCodeId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [StringLength(255)]
    public string TechnicalCodeName { get; set; }

    [InverseProperty("TechnicalCode")]
    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
