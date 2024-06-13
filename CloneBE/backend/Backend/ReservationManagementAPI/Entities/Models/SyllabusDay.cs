using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class SyllabusDay
{
    public string SyllabusDayId { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? DayNo { get; set; }

    public string SyllabusId { get; set; }

    public virtual Syllabus Syllabus { get; set; }

    public virtual ICollection<SyllabusUnit> SyllabusUnits { get; set; } = new List<SyllabusUnit>();
}
