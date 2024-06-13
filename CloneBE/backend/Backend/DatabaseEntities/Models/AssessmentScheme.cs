using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("AssessmentScheme")]
public partial class AssessmentScheme
{
    [Required]
    [Column("assesmentSchemeId")]
    [StringLength(36)]
    public string AssesmentSchemeId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("assignment")]
    public double? Assignment { get; set; }

    [Column("final_practice")]
    public double? FinalPractice { get; set; }

    [Column("final")]
    public double? Final { get; set; }

    [Column("final_theory")]
    public double? FinalTheory { get; set; }

    [Column("gpa")]
    public double? Gpa { get; set; }

    [Column("quiz")]
    public double? Quiz { get; set; }

    [Column("syllabus_id")]
    [StringLength(36)]
    public string SyllabusId { get; set; }

    [ForeignKey("SyllabusId")]
    [InverseProperty("AssessmentSchemes")]
    public virtual Syllabus Syllabus { get; set; }
}
