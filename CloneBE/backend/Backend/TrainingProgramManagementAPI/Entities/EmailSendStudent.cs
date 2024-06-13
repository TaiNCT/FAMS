using System;
using System.Collections.Generic;

namespace TrainingProgramManagementAPI.Entities;

public partial class EmailSendStudent
{
    public string EmailSendStudentId { get; set; } = null!;

    public int Id { get; set; }

    public string ReceiverId { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public int ReceiverType { get; set; }

    public virtual EmailSend Email { get; set; } = null!;

    public virtual Student Receiver { get; set; } = null!;
}
