using StudentInfoManagementAPI.DTO;

namespace StudentInfoManagementAPI.Service
{
	public interface IUploadFileDL
	{
		Task<bool> HasDuplicates(IFormFile file);
		Task HandleReplaceOption(string classId);
		public Task UploadOption(UploadExcelFileRequest request, string Path);
		public Task<UploadExcelFileResponse> UploadXMLFile(UploadExcelFileRequest request, string Path);
	}
}
