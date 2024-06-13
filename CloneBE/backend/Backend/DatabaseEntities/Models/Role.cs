using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("Role")]
[Index("RoleId", Name = "UQ__Role__CD98462BD96799FB", IsUnique = true)]
[Index("RoleName", Name = "role_name_unique", IsUnique = true)]
public partial class Role
{
    [Required]
    [Column("roleId")]
    [StringLength(36)]
    public string RoleId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(36)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(255)]
    public string RoleName { get; set; }

    [InverseProperty("Role")]
    public virtual RolePermission RolePermission { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
