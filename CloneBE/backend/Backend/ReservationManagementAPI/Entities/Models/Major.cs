using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class Major
{
    public string MajorId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
