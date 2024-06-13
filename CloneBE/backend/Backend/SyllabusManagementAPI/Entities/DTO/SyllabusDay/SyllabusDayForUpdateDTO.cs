using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO.SyllabusDay
{
	public class SyllabusDayForUpdateDTO
	{
		public string? CreatedBy { get; set; }

		public int? DayNo { get; set; }

		public virtual ICollection<SyllabusUnitForCreationDTO> SyllabusUnits { get; set; } = new List<SyllabusUnitForCreationDTO>();

	}
}
