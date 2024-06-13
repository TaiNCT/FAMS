using Entities;
using System.ComponentModel.DataAnnotations;

namespace EmailInformAPI.DTO;

public class EmailSendStudentDTO
{


    public int ReceiverType { get; set; }
    public virtual StudentDTO Receiver { get; set; } = null!;

    public string ReceiverRole { get; internal set; }
}
