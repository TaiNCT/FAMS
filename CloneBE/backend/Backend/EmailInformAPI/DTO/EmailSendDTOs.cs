using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailInformAPI.DTO
{
    public class EmailSendDTOs
    {
        [Column("emailSendId")]
        [StringLength(36)]
        public string? EmailSendId { get; set; } = null!;

        [Key]
        [Column("id")]
        public int? Id { get; set; }

        [StringLength(36)]
        public string TemplateId { get; set; } = null!;

        [StringLength(36)]
        public string? SenderId { get; set; }

        public string? EmailName { get; set; }

        public string? Description { get; set; }

        public string? FullName { get; set; } = null!;

        public string? Role { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime SendDate { get; set; }

        public int ReceiverType { get; set; }

        [NotMapped]
        public string Subject
        {
            get
            {
                // Tách nội dung thành subject và body
                string[] parts = Content.Split(';');
                if (parts.Length > 0)
                {
                    return parts[0];
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                // Khi gán giá trị cho subject, cập nhật nội dung content
                Content = value + ";" + Body;
            }
        }

        [NotMapped]
        public string Body
        {
            get
            {
                // Tách nội dung thành subject và body
                string[] parts = Content.Split(';');
                if (parts.Length > 1)
                {
                    return string.Join(";", parts, 1, parts.Length - 1);
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                // Khi gán giá trị cho body, cập nhật nội dung content
                Content = Subject + ";" + value;
            }
        }
    }
}
