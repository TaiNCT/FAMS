using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class RolePermission
{
    public string RoleId { get; set; } = null!;

    public string PermissionId { get; set; } = null!;

    public virtual UserPermission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
