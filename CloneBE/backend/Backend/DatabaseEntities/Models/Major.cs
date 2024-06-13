using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Major")]
[Index("Name", Name = "FK_MAJOR_NAME", IsUnique = true)]
[Index("MajorId", Name = "UQ__Major__A5B1B4B5AB7F7104", IsUnique = true)]
public partial class Major
{
    [Required]
    [Column("majorId")]
    [StringLength(36)]
    public string MajorId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    [InverseProperty("Major")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
