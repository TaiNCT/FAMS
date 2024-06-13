using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Fsu
{
    public int Id { get; set; }

    public string FsuId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
