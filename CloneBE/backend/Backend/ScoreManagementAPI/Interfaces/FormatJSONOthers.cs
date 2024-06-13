using System.ComponentModel.DataAnnotations;

namespace ScoreManagementAPI.Interfaces
{
    public class FormatJSONOthers
    {
        [Required]
        public string classid { get; set; }

        [Required]
        public string studentid { get; set;  }
        [Required]
        public string university { get; set; }

        [Required]
        public decimal gpa { get; set; }

        [Required]
        public string major { get; set; }

        [Required]
        public string recer {  get; set; }

        [Required]
        public DateTime gradtime {  get; set; }
    }
}
