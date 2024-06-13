using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class ClassUser
{
    public string UserId { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public string? UserType { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
