using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("SyllabusUnit")]
[Index("SyllabusUnitId", Name = "UQ__Syllabus__D5A44901F7DFBD8C", IsUnique = true)]
public partial class SyllabusUnit
{
    [Required]
    [Column("syllabusUnitId")]
    [StringLength(36)]
    public string SyllabusUnitId { get; set; }

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

    [Column("duration")]
    public int? Duration { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("unit_no")]
    public int UnitNo { get; set; }

    [Column("syllabus_day_id")]
    [StringLength(36)]
    public string SyllabusDayId { get; set; }

    [ForeignKey("SyllabusDayId")]
    [InverseProperty("SyllabusUnits")]
    public virtual SyllabusDay SyllabusDay { get; set; }

    [InverseProperty("SyllabusUnit")]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
