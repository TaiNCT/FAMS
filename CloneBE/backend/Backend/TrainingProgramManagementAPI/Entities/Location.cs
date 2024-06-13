using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class Location
{
    public string LocationId { get; set; } = null!;

    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
