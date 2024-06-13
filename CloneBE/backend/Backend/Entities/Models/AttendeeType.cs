using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class AttendeeType
{
    public int Id { get; set; }

    public string AttendeeTypeId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AttendeeTypeName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
