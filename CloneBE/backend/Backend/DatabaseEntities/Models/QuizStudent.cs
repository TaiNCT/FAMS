using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("QuizStudent")]
public partial class QuizStudent
{
    [Required]
    [Column("quizStudentId")]
    [StringLength(36)]
    public string QuizStudentId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(36)]
    public string StudentId { get; set; }

    [StringLength(36)]
    public string QuizId { get; set; }

    [Column(TypeName = "double")]
    public double? Score { get; set; }

    public DateTime? SubmissionDate { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("QuizStudents")]
    public virtual Quiz Quiz { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("QuizStudents")]
    public virtual Student Student { get; set; }
}
