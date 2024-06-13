using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class SyllabusUnit
{
    public int Id { get; set; }

    public string SyllabusUnitId { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? Duration { get; set; }

    public string Name { get; set; } = null!;

    public int UnitNo { get; set; }

    public string? SyllabusDayId { get; set; }

    public virtual ICollection<ClassTrainerLocation> ClassTrainerLocations { get; set; } = new List<ClassTrainerLocation>();

    [JsonIgnore]
    public virtual SyllabusDay? SyllabusDay { get; set; }

    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
