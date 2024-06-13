using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("UserPermission")]
[Index("UserPermissionId", Name = "UQ__UserPerm__0E30AD2E4AF9A398", IsUnique = true)]
public partial class UserPermission
{
    [Required]
    [Column("userPermissionId")]
    [StringLength(36)]
    public string UserPermissionId { get; set; }

    [Key]
    public int Id { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedTime { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedTime { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("Permission")]
    public virtual RolePermission RolePermission { get; set; }
}
