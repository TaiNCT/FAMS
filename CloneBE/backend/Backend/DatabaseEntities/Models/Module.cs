using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Module")]
[Index("ModuleId", Name = "UQ__Module__8EEC8E161230F0B0", IsUnique = true)]
public partial class Module
{
    [Required]
    [Column("moduleId")]
    [StringLength(36)]
    public string ModuleId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    public string ModuleName { get; set; }

    public DateTime CreatedDate { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [InverseProperty("Module")]
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    [InverseProperty("Module")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [InverseProperty("Module")]
    public virtual ICollection<StudentModule> StudentModules { get; set; } = new List<StudentModule>();
}
