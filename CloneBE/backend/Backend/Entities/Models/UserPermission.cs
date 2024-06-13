using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class UserPermission
{
    public int Id { get; set; }

    public string UserPermissionId { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; } = null!;
}
