using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("TrainingMaterial")]
[Index("TrainingMaterialId", Name = "UQ__Training__E3CB00D617BDF3BA", IsUnique = true)]
public partial class TrainingMaterial
{
    [Required]
    [Column("trainingMaterialId")]
    [StringLength(36)]
    public string TrainingMaterialId { get; set; }

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

    [Required]
    [Column("file_name")]
    [StringLength(255)]
    public string FileName { get; set; }

    [Column("is_file")]
    public bool IsFile { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("url")]
    [StringLength(255)]
    public string Url { get; set; }

    [Column("unit_chapter_id")]
    [StringLength(36)]
    public string UnitChapterId { get; set; }

    [ForeignKey("UnitChapterId")]
    [InverseProperty("TrainingMaterials")]
    public virtual UnitChapter UnitChapter { get; set; }
}
