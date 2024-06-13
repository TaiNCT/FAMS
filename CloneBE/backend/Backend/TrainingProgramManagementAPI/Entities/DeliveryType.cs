using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class DeliveryType
{
    public string DeliveryTypeId { get; set; } = null!;

    public int Id { get; set; }

    public string Descriptions { get; set; } = null!;

    public string? Icon { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
