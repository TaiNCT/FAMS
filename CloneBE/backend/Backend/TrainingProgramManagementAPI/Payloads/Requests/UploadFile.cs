using System.ComponentModel.DataAnnotations;
using TrainingProgramManagementAPI.Common.Enums;

namespace TrainingProgramManagementAPI.Payloads.Requests
{
    public class UploadFile
    {
        public class UploadFileRequest
        {
            public string CreatedBy { get; set; } = string.Empty;

            public IFormFile? File { get; set; }

            public string EncodingType { get; set; } = string.Empty;

            public string ColumnSeperator { get; set; } = string.Empty;

            [Required(ErrorMessage = "Scanning field is required.")]
            public string? Scanning { get; set; }

            public DuplicateHandleProgram? DuplicateHandle { get; set; }

        }

    }
}
