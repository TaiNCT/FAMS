using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class StudentClass
{
    public int Id { get; set; }

    public string StudentClassId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public string AttendingStatus { get; set; } = null!;

    public int Result { get; set; }

    public decimal FinalScore { get; set; }

    public int Gpalevel { get; set; }

    public int CertificationStatus { get; set; }

    public DateTime? CertificationDate { get; set; }

    public int Method { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
