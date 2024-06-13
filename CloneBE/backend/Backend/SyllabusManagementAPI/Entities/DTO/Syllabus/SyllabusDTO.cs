using Entities.Models;

namespace SyllabusManagementAPI.Entities.DTO
{
    public record class SyllabusDTO
    {
        public string SyllabusId { get; set; } = null!;

        public string TopicCode { get; set; } = null!;

        public string TopicName { get; set; } = null!;

        public string Version { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? AttendeeNumber { get; set; }

        public string? Level { get; set; }

        public string TechnicalRequirement { get; set; } = null!;

        public string CourseObjective { get; set; } = null!;

        public string DeliveryPrinciple { get; set; } = null!;

        public int? Days { get; set; }

        public double? Hours { get; set; }

        public string Status { get; set; } = null!;
	}
}
