using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class StudentClass
{
    public string StudentClassId { get; set; } = null!;

    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public int AttendingStatus { get; set; }

    public int Result { get; set; }

    public decimal FinalScore { get; set; }

    public int Gpalevel { get; set; }

    public int CertificationStatus { get; set; }

    public DateTime? CertificationDate { get; set; }

    public int Method { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
