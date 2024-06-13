using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("OutputStandard")]
[Index("OutputStandardId", Name = "UQ__OutputSt__BED5012D157BD6B3", IsUnique = true)]
public partial class OutputStandard
{
    [Required]
    [Column("outputStandardId")]
    [StringLength(36)]
    public string OutputStandardId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("code")]
    [StringLength(255)]
    public string Code { get; set; }

    [Required]
    [Column("descriptions")]
    [StringLength(255)]
    public string Descriptions { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("OutputStandard")]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
