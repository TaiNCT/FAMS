using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("TrainingProgram")]
[Index("TrainingProgramCode", Name = "UQ__Training__8245E6A3E1041402", IsUnique = true)]
public partial class TrainingProgram
{
    [Required]
    [StringLength(36)]
    public string TrainingProgramCode { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? Days { get; set; }

    public int? Hours { get; set; }

    public TimeOnly StartTime { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    [StringLength(36)]
    public string UserId { get; set; }

    [StringLength(36)]
    public string TechnicalCodeId { get; set; }

    [StringLength(36)]
    public string TechnicalGroupId { get; set; }

    [InverseProperty("TrainingProgramCodeNavigation")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    [ForeignKey("TechnicalCodeId")]
    [InverseProperty("TrainingPrograms")]
    public virtual TechnicalCode TechnicalCode { get; set; }

    [ForeignKey("TechnicalGroupId")]
    [InverseProperty("TrainingPrograms")]
    public virtual TechnicalGroup TechnicalGroup { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TrainingPrograms")]
    public virtual User User { get; set; }

    [ForeignKey("TrainingProgramCode")]
    [InverseProperty("TrainingProgramCodes")]
    public virtual ICollection<Syllabus> Syllabi { get; set; } = new List<Syllabus>();
}
