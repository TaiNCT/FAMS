﻿using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Role
{
    public string RoleId { get; set; }

    public int Id { get; set; }

    public string Title { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string RoleName { get; set; }

    public virtual RolePermission RolePermission { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}