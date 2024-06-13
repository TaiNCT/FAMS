namespace SyllabusManagementAPI.Entities.DTO.OutputStandard
{
	public class OutputStandardDTO
	{
		public string OutputStandardId { get; set; }

		public int Id { get; set; }

		public string Code { get; set; }

		public string Descriptions { get; set; }

		public string Name { get; set; } = null!;
	}
}
