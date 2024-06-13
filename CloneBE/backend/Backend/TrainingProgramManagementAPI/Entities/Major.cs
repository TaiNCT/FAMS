using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class Major
{
    public string MajorId { get; set; } = null!;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
