using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class DeliveryType
{
    public string DeliveryTypeId { get; set; }

    public int Id { get; set; }

    public string Descriptions { get; set; }

    public string Icon { get; set; }

    public string Name { get; set; }

    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
