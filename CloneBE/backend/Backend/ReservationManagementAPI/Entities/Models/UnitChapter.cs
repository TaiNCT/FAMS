using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class UnitChapter
{
    public string UnitChapterId { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int ChapterNo { get; set; }

    public int? Duration { get; set; }

    public bool IsOnline { get; set; }

    public string Name { get; set; }

    public string DeliveryTypeId { get; set; }

    public string OutputStandardId { get; set; }

    public string SyllabusUnitId { get; set; }

    public virtual DeliveryType DeliveryType { get; set; }

    public virtual OutputStandard OutputStandard { get; set; }

    public virtual SyllabusUnit SyllabusUnit { get; set; }

    public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
}
