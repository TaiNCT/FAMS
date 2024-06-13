using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class AttendeeType
{
    public string AttendeeTypeId { get; set; }

    public int Id { get; set; }

    public string Description { get; set; }

    public string AttendeeTypeName { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
