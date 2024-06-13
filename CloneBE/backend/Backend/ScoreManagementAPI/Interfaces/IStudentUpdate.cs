using System.ComponentModel.DataAnnotations;

namespace ScoreManagementAPI.Interfaces
{
    public class ILocation
    {
        [Required]
        public string location { get; set; }
        [Required]
        public string permanent_res { get; set; }

    }

    public class IStudentUpdate
    {

        // "General" section
        [Required]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int gender { get; set; }
        [Required]
        public DateTime dob { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string location { get; set; }

        // "Other" section
        [Required]
        public string university { get; set; }
        [Required]
        public string major { get; set; }
        [Required]
        public int gpa { get; set; }
        [Required]
        public string recer { get; set; }
        [Required]
        public DateTime gradtime { get; set; }

        // "Scores" Section
        // "Quiz" stuffsz
        [Required]
        public double html { get; set; }
        [Required]
        public double css { get; set; }
        [Required]
        public double quiz3 { get; set; }
        [Required]
        public double quiz4 { get; set; }
        [Required]
        public double quiz5 { get; set; }
        [Required]
        public double quiz6 { get; set; }

        // "ASM" stuffs
        [Required]
        public double practice1 { get; set; }
        [Required]
        public double practice2 { get; set; }
        [Required]
        public double practice3 { get; set; }

        // "Mock" stuffs
        [Required]
        public double mock { get; set; }
        [Required]
        public double final { get; set; }
        [Required]
        public double gpa2 { get; set; }
        [Required]
        public int level { get; set; }

    }
}
