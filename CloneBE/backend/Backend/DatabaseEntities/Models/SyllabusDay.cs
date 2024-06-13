using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("SyllabusDay")]
[Index("SyllabusDayId", Name = "UQ__Syllabus__6F1A1381B348F5E2", IsUnique = true)]
public partial class SyllabusDay
{
    [Required]
    [Column("syllabusDayId")]
    [StringLength(36)]
    public string SyllabusDayId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created_by")]
    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("modified_by")]
    [StringLength(36)]
    public string ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("day_no")]
    public int? DayNo { get; set; }

    [Column("syllabus_id")]
    [StringLength(36)]
    public string SyllabusId { get; set; }

    [ForeignKey("SyllabusId")]
    [InverseProperty("SyllabusDays")]
    public virtual Syllabus Syllabus { get; set; }

    [InverseProperty("SyllabusDay")]
    public virtual ICollection<SyllabusUnit> SyllabusUnits { get; set; } = new List<SyllabusUnit>();
}
