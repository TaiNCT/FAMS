using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Assignment")]
[Index("AssignmentId", Name = "UQ__Assignme__52C21821131FFA63", IsUnique = true)]
public partial class Assignment
{
    [Required]
    [Column("assignmentId")]
    [StringLength(36)]
    public string AssignmentId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string ModuleId { get; set; }

    [Required]
    public string AssignmentName { get; set; }

    public int AssignmentType { get; set; }

    public DateTime DueDate { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("Assignments")]
    public virtual Module Module { get; set; }

    [InverseProperty("Assignment")]
    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
