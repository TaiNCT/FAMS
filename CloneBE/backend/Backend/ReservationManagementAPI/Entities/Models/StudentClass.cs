using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class StudentClass
{
    public string StudentClassId { get; set; }

    public int Id { get; set; }

    public string StudentId { get; set; }

    public string ClassId { get; set; }

    public string AttendingStatus { get; set; }

    public int Result { get; set; }

    public decimal FinalScore { get; set; }

    public int Gpalevel { get; set; }

    public int CertificationStatus { get; set; }

    public DateTime? CertificationDate { get; set; }

    public int Method { get; set; }

    public virtual Class Class { get; set; }

    public virtual Student Student { get; set; }
}
