using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class AssessmentSchemeForCreationDTO
    {
        [Required(ErrorMessage = "Assignment is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? Assignment { get; set; }

        [Required(ErrorMessage = "FinalPractice is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? FinalPractice { get; set; }

        [Required(ErrorMessage = "Final is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? Final { get; set; }

        [Required(ErrorMessage = "FinalTheory is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? FinalTheory { get; set; }

        [Required(ErrorMessage = "Gpa is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? Gpa { get; set; }

        [Required(ErrorMessage = "Quiz is required")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public double? Quiz { get; set; }
    }
}