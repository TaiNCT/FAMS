using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class ClassUser
{
    public string UserId { get; set; }

    public string ClassId { get; set; }

    public string UserType { get; set; }

    public virtual Class Class { get; set; }

    public virtual User User { get; set; }
}
