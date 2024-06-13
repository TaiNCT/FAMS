using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreManagementAPI.Models;

[Table("EmailSendStudent")]
[Index("EmailSendStudentId", Name = "UQ__EmailSen__2D96D8D698AB8010", IsUnique = true)]
public partial class EmailSendStudent
{
    [Required]
    [Column("emailSendStudentId")]
    [StringLength(36)]
    public string EmailSendStudentId { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(36)]
    public string ReceiverId { get; set; }

    [Required]
    [StringLength(36)]
    public string EmailId { get; set; }

    public int ReceiverType { get; set; }

    [ForeignKey("EmailId")]
    [InverseProperty("EmailSendStudents")]
    public virtual EmailSend Email { get; set; }

    [ForeignKey("ReceiverId")]
    [InverseProperty("EmailSendStudents")]
    public virtual Student Receiver { get; set; }
}
