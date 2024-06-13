using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[PrimaryKey("RoleId", "PermissionId")]
[Table("RolePermission")]
[Index("RoleId", Name = "UQ__RolePerm__8AFACE1B8EC32168", IsUnique = true)]
[Index("PermissionId", Name = "UQ__RolePerm__EFA6FB2EE63A2F67", IsUnique = true)]
public partial class RolePermission
{
    [Key]
    [StringLength(36)]
    public string RoleId { get; set; }

    [Key]
    [StringLength(36)]
    public string PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("RolePermission")]
    public virtual UserPermission Permission { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermission")]
    public virtual Role Role { get; set; }
}
