using SyllabusManagementAPI.Entities.DTO.AssessmentScheme;
using SyllabusManagementAPI.Entities.DTO.SyllabusDay;
using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class SyllabusForUpdateDTO
    {
        [Required(ErrorMessage = "SyllabusId is required")]
        public string SyllabusId { get; set; } = null!;
        public string? TopicCode { get; set; } = null!;

        [Required(ErrorMessage = "TopicName is required")]
        public string TopicName { get; set; } = null!;

        [Required(ErrorMessage = "Version is required")]
        public string Version { get; set; } = null!;

        [Required(ErrorMessage = "CreatedBy is required")]
        public string? CreatedBy { get; set; }

        [Required(ErrorMessage = "AttendeeNumber is required")]
        public int? AttendeeNumber { get; set; }

        [Required(ErrorMessage = "Level is required")]
        public string? Level { get; set; }

        [Required(ErrorMessage = "TechnicalRequirement is required")]
        public string TechnicalRequirement { get; set; } = null!;

        [Required(ErrorMessage = "CourseObjective is required")]
        public string CourseObjective { get; set; } = null!;

        public string DeliveryPrinciple { get; set; } = null!;

        public int? Days { get; set; }

        public double? Hours { get; set; }

        public virtual ICollection<AssessmentSchemeForCreationDTO> AssessmentSchemes { get; set; } = new List<AssessmentSchemeForCreationDTO>();

        public virtual ICollection<SyllabusDayForCreationDTO> SyllabusDays { get; set; } = new List<SyllabusDayForCreationDTO>();
    }
}
