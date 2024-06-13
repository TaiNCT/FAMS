using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("UserClass")]
[Index("ClassId", Name = "IX_UserClass_ClassId")]
[Index("UserId", Name = "IX_UserClass_UserId")]
public partial class UserClass
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int ClassId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("UserClasses")]
    public virtual Class Class { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserClasses")]
    public virtual User User { get; set; } = null!;
}
