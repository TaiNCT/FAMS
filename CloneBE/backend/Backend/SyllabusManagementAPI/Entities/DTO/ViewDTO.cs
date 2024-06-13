using static SyllabusManagementAPI.Entities.DTO.ViewDTO;

namespace SyllabusManagementAPI.Entities.DTO
{
    public class ViewDTO
    {
        public class HeaderViewModel
        {
            public string TopicCode { get; set; }
            public string TopicName { get; set; }
            public string Version { get; set; }
            public string? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string Status { get; set; }
        }

        public class GeneralViewModel
        {
            public int? AttendeeNumber { get; set; }
            public string Level { get; set; }
            public string TechnicalRequirement { get; set; }
            public string CourseObjective { get; set; }
            public int? Days { get; set; }
            public double? Hours { get; set; }
            public string OutputStandardCode { get; set; }
        }

        public class SyllabusDayViewModel
        {
            public int DayNo { get; set; }
            public IEnumerable<SyllabusUnitViewModel> SyllabusUnits { get; set; } = new List<SyllabusUnitViewModel>();
        }

        public class SyllabusUnitViewModel
        {
            public int UnitNo { get; set; }
            public string Name { get; set; }
            public int? Duration { get; set; }
            public IEnumerable<UnitChapterViewModel> UnitChapters { get; set; } = new List<UnitChapterViewModel>();
        }

        public class UnitChapterViewModel
        {
            public string Name { get; set; }
            public int? Duration { get; set; }
            public bool IsOnline { get; set; }
            public string OutputStandardId {  get; set; }
            public string DeliveryTypeId { get; set;}
            public string OutputStandardName { get; set; }
            public string DeliveryTypeName { get; set; }
        }

        public class AssessmentSchemeViewModel
        {
            public double? Assignment { get; set; }
            public double? FinalTheory { get; set; }
            public double? FinalPractice { get; set; }
            public double? Final { get; set; }
            public double? Gpa { get; set; }
            public double? Quiz { get; set; }
            public string? DeliveryPrinciple { get; set; }
        }
    }
}
