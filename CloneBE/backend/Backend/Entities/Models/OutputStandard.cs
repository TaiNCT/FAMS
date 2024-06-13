using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class OutputStandard
{
    public int Id { get; set; }

    public string OutputStandardId { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
