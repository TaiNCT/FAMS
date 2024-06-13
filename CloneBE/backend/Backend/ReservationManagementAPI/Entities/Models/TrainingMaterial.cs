using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class TrainingMaterial
{
    public string TrainingMaterialId { get; set; }

    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string FileName { get; set; }

    public bool IsFile { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string UnitChapterId { get; set; }

    public virtual UnitChapter UnitChapter { get; set; }
}
