using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Syllabus")]
[Index("SyllabusId", Name = "UQ__Syllabus__915EDF816714E22A", IsUnique = true)]
[Index("TopicCode", Name = "topic_code_unique", IsUnique = true)]
public partial class Syllabus
{
    [Required]
    [Column("syllabusId")]
    [StringLength(36)]
    public string SyllabusId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("topic_code")]
    [StringLength(20)]
    public string TopicCode { get; set; }

    [Required]
    [Column("topic_name")]
    [StringLength(255)]
    public string TopicName { get; set; }

    [Required]
    [Column("version")]
    [StringLength(50)]
    public string Version { get; set; }

    [Column("created_by")]
    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("modified_by")]
    [StringLength(36)]
    public string ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("attendee_number")]
    public int? AttendeeNumber { get; set; }

    [Column("level")]
    [StringLength(50)]
    public string Level { get; set; }

    [Required]
    [Column("technical_requirement")]
    public string TechnicalRequirement { get; set; }

    [Required]
    [Column("course_objective")]
    public string CourseObjective { get; set; }

    [Required]
    [Column("delivery_principle")]
    public byte[] DeliveryPrinciple { get; set; }

    [Column("days")]
    public int? Days { get; set; }

    [Column("hours")]
    public double? Hours { get; set; }

    [InverseProperty("Syllabus")]
    public virtual ICollection<AssessmentScheme> AssessmentSchemes { get; set; } = new List<AssessmentScheme>();

    [InverseProperty("Syllabus")]
    public virtual ICollection<SyllabusDay> SyllabusDays { get; set; } = new List<SyllabusDay>();

    [ForeignKey("SyllabusId")]
    [InverseProperty("Syllabi")]
    public virtual ICollection<TrainingProgram> TrainingProgramCodes { get; set; } = new List<TrainingProgram>();
}
