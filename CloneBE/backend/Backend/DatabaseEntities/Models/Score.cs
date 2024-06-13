using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Score")]
[Index("ScoreId", Name = "UQ__Score__B56A0C8CE82A11F4", IsUnique = true)]
public partial class Score
{
    [Required]
    [Column("scoreId")]
    [StringLength(36)]
    public string ScoreId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string StudentId { get; set; }

    [Required]
    [StringLength(36)]
    public string AssignmentId { get; set; }

    [Column("Score", TypeName = "double")]
    public double? Score1 { get; set; }

    public DateTime? SubmissionDate { get; set; }

    [ForeignKey("AssignmentId")]
    [InverseProperty("Scores")]
    public virtual Assignment Assignment { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Scores")]
    public virtual Student Student { get; set; }
}
