using Entities.Models;

namespace Entities.DTO.SyllabusDay
{
	public class SyllabusDayDTO
	{
		public string SyllabusDayId { get; set; }

		public int Id { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public bool IsDeleted { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public int? DayNo { get; set; }

		public string SyllabusId { get; set; }

		public virtual Syllabus Syllabus { get; set; }
	}
}
