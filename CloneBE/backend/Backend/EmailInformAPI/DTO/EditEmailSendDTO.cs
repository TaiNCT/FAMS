using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailInformAPI.DTO
{
    public class EditEmailSendDTO
    {

        [Required]
        public string templateId {  get; set; }

        public string? SenderId { get; set; }

        public string Content { get; set; } = null!;
    }
}
