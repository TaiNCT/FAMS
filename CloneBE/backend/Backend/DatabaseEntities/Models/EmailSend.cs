using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("EmailSend")]
[Index("EmailSendId", Name = "UQ__EmailSen__4B3B46D7861D7F1D", IsUnique = true)]
public partial class EmailSend
{
    [Required]
    [Column("emailSendId")]
    [StringLength(36)]
    public string EmailSendId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string TemplateId { get; set; }

    [StringLength(36)]
    public string SenderId { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime SendDate { get; set; }

    public int ReceiverType { get; set; }

    [InverseProperty("Email")]
    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    [ForeignKey("SenderId")]
    [InverseProperty("EmailSends")]
    public virtual User Sender { get; set; }

    [ForeignKey("TemplateId")]
    [InverseProperty("EmailSends")]
    public virtual EmailTemplate Template { get; set; }
}
