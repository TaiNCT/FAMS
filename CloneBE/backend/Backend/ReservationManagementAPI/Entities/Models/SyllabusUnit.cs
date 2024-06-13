using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class SyllabusUnit
{
    public string SyllabusUnitId { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? Duration { get; set; }

    public string Name { get; set; }

    public int UnitNo { get; set; }

    public string SyllabusDayId { get; set; }

    public virtual SyllabusDay SyllabusDay { get; set; }

    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
