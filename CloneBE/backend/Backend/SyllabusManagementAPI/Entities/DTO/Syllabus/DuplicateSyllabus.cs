namespace SyllabusManagementAPI.Entities.DTO.Syllabus
{
    public class DuplicateSyllabusRequest
    {
		public string SyllabusId { get; set; } = null!;
		public string? TopicCode { get; set; } = null!;
		public string? CreatedBy { get; set; } = string.Empty;
	}
}
