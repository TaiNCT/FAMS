using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("DeliveryType")]
[Index("Name", Name = "UQ__Delivery__72E12F1B533B3489", IsUnique = true)]
[Index("DeliveryTypeId", Name = "UQ__Delivery__BA19297B62D4D48E", IsUnique = true)]
public partial class DeliveryType
{
    [Required]
    [Column("deliveryTypeId")]
    [StringLength(36)]
    public string DeliveryTypeId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("descriptions")]
    [StringLength(255)]
    public string Descriptions { get; set; }

    [Column("icon")]
    [StringLength(255)]
    public string Icon { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [InverseProperty("DeliveryType")]
    public virtual ICollection<UnitChapter> UnitChapters { get; set; } = new List<UnitChapter>();
}
