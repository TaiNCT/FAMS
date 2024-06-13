using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class EmailSendStudent
{
    public string EmailSendStudentId { get; set; }

    public int Id { get; set; }

    public string ReceiverId { get; set; }

    public string EmailId { get; set; }

    public int ReceiverType { get; set; }

    public virtual EmailSend Email { get; set; }

    public virtual Student Receiver { get; set; }
}
