using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[PrimaryKey("UserId", "ClassId")]
[Table("ClassUser")]
public partial class ClassUser
{
    [Key]
    [StringLength(36)]
    public string UserId { get; set; }

    [Key]
    [StringLength(36)]
    public string ClassId { get; set; }

    [StringLength(50)]
    public string UserType { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("ClassUsers")]
    public virtual Class Class { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ClassUsers")]
    public virtual User User { get; set; }
}
