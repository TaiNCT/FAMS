using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Quiz")]
[Index("QuizId", Name = "UQ__Quiz__CFF54C3C93C8AD57", IsUnique = true)]
public partial class Quiz
{
    [Required]
    [Column("quizId")]
    [StringLength(36)]
    public string QuizId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(36)]
    public string ModuleId { get; set; }

    public string QuizName { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("Quizzes")]
    public virtual Module Module { get; set; }

    [InverseProperty("Quiz")]
    public virtual ICollection<QuizStudent> QuizStudents { get; set; } = new List<QuizStudent>();
}
