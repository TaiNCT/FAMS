using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class DeliveryType
{
    public int Id { get; set; }

    public string DeliveryTypeId { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public string? Icon { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
