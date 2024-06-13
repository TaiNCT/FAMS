using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class EmailSend
{
    public int Id { get; set; }

    public string EmailSendId { get; set; } = null!;

    public string TemplateId { get; set; } = null!;

    public string? SenderId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SendDate { get; set; }

    public int ReceiverType { get; set; }

    public virtual ICollection<EmailSendStudent> EmailSendStudents { get; set; } = new List<EmailSendStudent>();

    public virtual User? Sender { get; set; }

    public virtual EmailTemplate Template { get; set; } = null!;
}
