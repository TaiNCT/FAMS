using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Location
{
    public int Id { get; set; }

    public string LocationId { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ClassTrainerLocation> ClassTrainerLocations { get; set; } = new List<ClassTrainerLocation>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
