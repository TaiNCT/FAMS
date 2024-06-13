using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Fsu
{
    public int Id { get; set; }

    public string FsuId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
