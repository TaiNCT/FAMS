using Entities;

namespace EmailInformAPI.DTO
{
    public class EmailSendDTO
    {

        public string Content { get; set; } = null!;
        public DateTime SendDate { get; set; }
        public virtual UserDTO? Sender { get; set; }
        public virtual ICollection<EmailSendStudentDTO> EmailSendStudents { get; set; } = new List<EmailSendStudentDTO>();

    }
}
