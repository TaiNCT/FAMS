using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailInformAPI.DTO
{
    public class AddEmailSendDTO
    {
        public string? EmailSendId { get; set; } = null!;
        public int? Id { get; set; }
        public string EmailTemplateId { get; set; } = null!;
        public string? SenderId { get; set; }
        public string? FullName { get; set; } = null!;
        public string? Role { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendDate { get; set; }
        public int ReceiverType { get; set; }
        public int IdStatus { get; set; }

    }
}