using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class EmailSendStudent
{
    public int Id { get; set; }

    public string EmailSendStudentId { get; set; } = null!;

    public string ReceiverId { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public int ReceiverType { get; set; }

    public virtual EmailSend Email { get; set; } = null!;

    public virtual Student Receiver { get; set; } = null!;
}
