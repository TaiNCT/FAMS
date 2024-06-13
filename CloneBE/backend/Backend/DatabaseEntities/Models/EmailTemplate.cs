using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("EmailTemplate")]
[Index("EmailTemplateId", Name = "UQ__EmailTem__C443B510FB556726", IsUnique = true)]
public partial class EmailTemplate
{
    [Required]
    [Column("emailTemplateId")]
    [StringLength(36)]
    public string EmailTemplateId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int Type { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    [StringLength(36)]
    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    [StringLength(36)]
    public string UpdatedBy { get; set; }

    [InverseProperty("Template")]
    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();
}
