using System.ComponentModel.DataAnnotations;

namespace EmailInformAPI.DTO
{
    public class EmailScheduleDTO
    {
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string lastmsg { get; set; }
        [Required]
        public string firstmsg { get; set; }
        [Required]
        public string subject { get; set; }

        public Score? body { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string recipient { get; set; }

        // Options to pick
        public ExtraOption ?options {  get; set; }
    }
}
