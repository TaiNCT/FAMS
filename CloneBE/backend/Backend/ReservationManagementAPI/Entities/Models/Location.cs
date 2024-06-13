using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Location
{
    public string LocationId { get; set; }

    public int Id { get; set; }

    public string Address { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
