using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO.SyllabusUnit
{
	public class SyllabusUnitForUpdateDTO
	{
		public string? CreatedBy { get; set; }

		public int? Duration { get; set; }

		public string Name { get; set; } = null!;

		public int UnitNo { get; set; }

		public virtual ICollection<UnitChapterForCreationDTO> UnitChapters { get; set; } = new List<UnitChapterForCreationDTO>();
	}
}
