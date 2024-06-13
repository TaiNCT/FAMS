namespace ClassManagementAPI.Dto.SyllabusDTO
{
    public class GetSyllabusDTO
    {
        public string SyllabusId { get; set; } = null!;

        public int Id { get; set; }

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

        public byte[] DeliveryPrinciple { get; set; } = null!;

        public int? Days { get; set; }

        public double? Hours { get; set; }

        public bool Status { get; set; }
    }
}
