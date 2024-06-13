using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class RolePermission
{
    public string RoleId { get; set; }

    public string PermissionId { get; set; }

    public virtual UserPermission Permission { get; set; }

    public virtual Role Role { get; set; }
}
