using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class RolePermission
{
    public string PermissionId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string Syllabus { get; set; } = null!;

    public string TrainingProgram { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string LearningMaterial { get; set; } = null!;

    public string UserManagement { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
