namespace SyllabusManagementAPI.Entities.DTO.SyllabusUnit
{
	public class SyllabusUnitDTO
	{
		public string SyllabusUnitId { get; set; }

		public int Id { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public bool IsDeleted { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public int? Duration { get; set; }

		public string Name { get; set; }

		public int UnitNo { get; set; }

		public string SyllabusDayId { get; set; }
	}
}
