using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class EmailSend
{
    public string EmailSendId { get; set; }

    public int Id { get; set; }

    public string TemplateId { get; set; }

    public string SenderId { get; set; }

    public string Content { get; set; }

    public DateTime SendDate { get; set; }

    public int ReceiverType { get; set; }

    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    public virtual User Sender { get; set; }

    public virtual EmailTemplate Template { get; set; }
}
