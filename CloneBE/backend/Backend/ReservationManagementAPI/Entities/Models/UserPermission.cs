using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class UserPermission
{
    public string UserPermissionId { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; }

    public virtual RolePermission RolePermission { get; set; }
}
