using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO.UnitChapter
{
	public class UnitChapterForUpdateDTO
	{
		public string? CreatedBy { get; set; }

		public int ChapterNo { get; set; }

		public int? Duration { get; set; }

		public bool IsOnline { get; set; }

		public string Name { get; set; } = null!;

		public string? DeliveryTypeId { get; set; }

		public string? OutputStandardId { get; set; }

		//public virtual DeliveryTypeForCreationDTO? DeliveryType { get; set; }

		//public virtual OutputStandardForCreationDTO? OutputStandard { get; set; }

	}
}
