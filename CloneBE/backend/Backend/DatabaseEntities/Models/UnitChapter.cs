using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("UnitChapter")]
[Index("UnitChapterId", Name = "UQ__UnitChap__A4B0833CB0354546", IsUnique = true)]
public partial class UnitChapter
{
    [Required]
    [Column("unitChapterId")]
    [StringLength(36)]
    public string UnitChapterId { get; set; }

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

    [Column("chapter_no")]
    public int ChapterNo { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("is_online")]
    public bool IsOnline { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("delivery_type_id")]
    [StringLength(36)]
    public string DeliveryTypeId { get; set; }

    [Column("output_standard_id")]
    [StringLength(36)]
    public string OutputStandardId { get; set; }

    [Column("syllabus_unit_id")]
    [StringLength(36)]
    public string SyllabusUnitId { get; set; }

    [ForeignKey("DeliveryTypeId")]
    [InverseProperty("UnitChapters")]
    public virtual DeliveryType DeliveryType { get; set; }

    [ForeignKey("OutputStandardId")]
    [InverseProperty("UnitChapters")]
    public virtual OutputStandard OutputStandard { get; set; }

    [ForeignKey("SyllabusUnitId")]
    [InverseProperty("UnitChapters")]
    public virtual SyllabusUnit SyllabusUnit { get; set; }

    [InverseProperty("UnitChapter")]
    public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
}
