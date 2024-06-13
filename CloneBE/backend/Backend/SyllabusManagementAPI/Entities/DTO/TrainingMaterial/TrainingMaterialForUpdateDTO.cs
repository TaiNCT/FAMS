using System.ComponentModel.DataAnnotations;

namespace SyllabusManagementAPI.Entities.DTO.TrainingMaterial
{
	public class TrainingMaterialForUpdateDTO
	{
		public string? CreatedBy { get; set; }

		public string FileName { get; set; } = null!;

		public bool IsFile { get; set; }

		public string Name { get; set; } = null!;

		public string? Url { get; set; }
	}
}
