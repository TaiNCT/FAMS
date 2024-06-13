using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("fsu")]
[Index("FsuId", Name = "UQ__fsu__E1FCEFCAB5A03117", IsUnique = true)]
public partial class Fsu
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("fsuId")]
    [StringLength(36)]
    public string FsuId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("Fsu")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
