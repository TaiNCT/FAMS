using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class TechnicalGroup
{
    public string TechnicalGroupId { get; set; }

    public int Id { get; set; }

    public string Description { get; set; }

    public string TechnicalGroupName { get; set; }

    public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
}
