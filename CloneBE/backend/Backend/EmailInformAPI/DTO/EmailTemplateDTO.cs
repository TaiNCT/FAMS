using Entities;

namespace EmailInformAPI.DTO
{
    public class EmailTemplateDTO
    {
        public string TemId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int IdType { get; set; }
        public string Type { get; set; } = null!;
        public int Status { get; set; }
        public string ApplyTo { get; set; } = null!;


    }
}
