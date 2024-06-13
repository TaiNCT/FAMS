using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class OutputStandard
{
    public string OutputStandardId { get; set; } = null!;

    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
