using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class UserPermission
{
    public string UserPermissionId { get; set; } = null!;

    public int Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; } = null!;

    public virtual RolePermission? RolePermission { get; set; }
}
