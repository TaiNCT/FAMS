using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class ReservedClass
{
    public string ReservedClassId { get; set; }

    public int Id { get; set; }

    public string StudentId { get; set; }

    public string ClassId { get; set; }

    public string Reason { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Class Class { get; set; }

    public virtual Student Student { get; set; }
}
