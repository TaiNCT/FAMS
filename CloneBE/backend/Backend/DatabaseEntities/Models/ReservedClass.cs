using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("ReservedClass")]
[Index("ReservedClassId", Name = "UQ__Reserved__12EF4C50126A62C8", IsUnique = true)]
public partial class ReservedClass
{
    [Required]
    [Column("reservedClassId")]
    [StringLength(36)]
    public string ReservedClassId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(36)]
    public string StudentId { get; set; }

    [Required]
    [StringLength(36)]
    public string ClassId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("ReservedClasses")]
    public virtual Class Class { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("ReservedClasses")]
    public virtual Student Student { get; set; }
}
