using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("StudentClass")]
[Index("StudentClassId", Name = "UQ__StudentC__114B9902B8A9BF95", IsUnique = true)]
public partial class StudentClass
{
    [Required]
    [Column("studentClassId")]
    [StringLength(36)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string StudentClassId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string StudentId { get; set; }

    [Required]
    [StringLength(36)]
    public string ClassId { get; set; }

    public int AttendingStatus { get; set; }

    public int Result { get; set; }

    [Column(TypeName = "double")]
    public double FinalScore { get; set; }

    [Column("GPALevel")]
    public int Gpalevel { get; set; }

    public int Method { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("StudentClasses")]
    public virtual Class Class { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentClasses")]
    public virtual Student Student { get; set; }
}
