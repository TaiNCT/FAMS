using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("AttendeeType")]
[Index("AttendeeTypeId", Name = "UQ__Attendee__114FA69277F2FFD0", IsUnique = true)]
[Index("AttendeeTypeName", Name = "attendee_type_unique", IsUnique = true)]
public partial class AttendeeType
{
    [Required]
    [Column("attendeeTypeId")]
    [StringLength(36)]
    public string AttendeeTypeId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string AttendeeTypeName { get; set; }

    [InverseProperty("AttendeeLevel")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
