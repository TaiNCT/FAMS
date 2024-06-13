using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Class")]
[Index("ClassId", Name = "UQ__Class__7577347F89972D9C", IsUnique = true)]
public partial class Class
{
    [Required]
    [Column("classId")]
    [StringLength(36)]
    public string ClassId { get; set; }

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

    [Required]
    [StringLength(255)]
    public string ClassStatus { get; set; }

    [Required]
    [StringLength(255)]
    public string ClassCode { get; set; }

    public int? Duration { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [StringLength(36)]
    public string ApprovedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApprovedDate { get; set; }

    [StringLength(36)]
    public string ReviewBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    public int AcceptedAttendee { get; set; }

    public int ActualAttendee { get; set; }

    [Required]
    [StringLength(255)]
    public string ClassName { get; set; }

    [Column("fsu_id")]
    [StringLength(36)]
    public string FsuId { get; set; }

    [StringLength(36)]
    public string LocationId { get; set; }

    [StringLength(36)]
    public string AttendeeLevelId { get; set; }

    [StringLength(36)]
    public string TrainingProgramCode { get; set; }

    public int PlannedAttendee { get; set; }

    [StringLength(30)]
    public string SlotTime { get; set; }

    [ForeignKey("AttendeeLevelId")]
    [InverseProperty("Classes")]
    public virtual AttendeeType AttendeeLevel { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();

    [ForeignKey("FsuId")]
    [InverseProperty("Classes")]
    public virtual Fsu Fsu { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Classes")]
    public virtual Location Location { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<ReservedClass> ReservedClasses { get; set; } = new List<ReservedClass>();

    [InverseProperty("Class")]
    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    [ForeignKey("TrainingProgramCode")]
    [InverseProperty("Classes")]
    public virtual TrainingProgram TrainingProgramCodeNavigation { get; set; }
}
