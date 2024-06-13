using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? RoleName { get; set; }

    public virtual RolePermission? RolePermission { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
