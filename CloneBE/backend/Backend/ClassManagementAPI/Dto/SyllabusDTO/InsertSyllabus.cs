namespace ClassManagementAPI.Dto.SyllabusDTO
{
    public class InsertSyllabus
    {
        public string TopicCode { get; set; } = null!;

        public string TopicName { get; set; } = null!;

        public string Version { get; set; } = null!;

        public string TechnicalRequirement { get; set; } = null!;

        public string CourseObjective { get; set; } = null!;

        public string DeliveryPrinciple { get; set; } = null!;

        public int? Days { get; set; }

        public double? Hours { get; set; }

        public string Status { get; set; }
    }
}
