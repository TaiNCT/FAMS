using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Location")]
[Index("LocationId", Name = "UQ__Location__30646B6F40CDA6A8", IsUnique = true)]
public partial class Location
{
    [Required]
    [Column("locationId")]
    [StringLength(36)]
    public string LocationId { get; set; }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
