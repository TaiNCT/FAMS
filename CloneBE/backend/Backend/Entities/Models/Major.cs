using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Major
{
    public int Id { get; set; }

    public string MajorId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
