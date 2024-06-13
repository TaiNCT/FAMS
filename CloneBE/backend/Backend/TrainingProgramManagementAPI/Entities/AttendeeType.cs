using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class AttendeeType
{
    public string AttendeeTypeId { get; set; } = null!;

    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string AttendeeTypeName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
