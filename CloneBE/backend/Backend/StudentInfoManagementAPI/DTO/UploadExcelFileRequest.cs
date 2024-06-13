namespace StudentInfoManagementAPI.DTO
{
	public class UploadExcelFileRequest
	{
		public IFormFile File { get; set; }
		public string? duplicateOption { get; set; }
		public string? classId { get; set; }
	}
}