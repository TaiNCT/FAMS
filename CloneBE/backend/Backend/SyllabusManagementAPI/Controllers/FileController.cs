using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace SyllabusManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        // Get file Directory
        [HttpGet, DisableRequestSizeLimit]
        [Route("get-template-path")]
        public async Task<IActionResult> GetTemplatePath()
        {
            try
            {
                var folderName = Path.Combine("Resources", "Templates");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var template = Directory.EnumerateFiles(pathToRead)
                    .Where(IsExcelFile)
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));
                return Ok(new { template });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private bool IsExcelFile(string fileName)
        {
            return fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                || fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);
        }

        // Download template based on file URL
        [HttpGet, DisableRequestSizeLimit]
        [Route("download-syllabus-template")]
        public async Task<IActionResult> DownloadSyllabusTemmplate([FromQuery] string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}