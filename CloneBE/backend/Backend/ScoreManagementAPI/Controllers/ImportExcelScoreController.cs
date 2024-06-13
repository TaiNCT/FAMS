using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Make sure to include this namespace for ILogger
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Repository;
using System;

namespace ScoreManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExcelScoreController : ControllerBase
    {
        private readonly IimportExcelScoreRepository _importExcelScoreRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<ImportExcelScoreController> _logger; // Declare the logger instance

        public ImportExcelScoreController(IimportExcelScoreRepository importExcelScoreRepository, IStudentRepository studentRepository, ILogger<ImportExcelScoreController> logger)
        {
            _importExcelScoreRepository = importExcelScoreRepository;
            _studentRepository = studentRepository;
            _logger = logger; // Inject the logger instance
        }

        // POST api/<ImportExcelScoreController>
        [HttpPost]
        public IActionResult UploadExcelFile([FromForm] ImportExcelScoreRequest request, [FromQuery] string option = "")
        {
            ImportExcelScoreResponse response = new ImportExcelScoreResponse();

            try
            {
                // Check if the file is missing
                if (request.File == null)
                {
                    response.IsSuccess = false;
                    response.Message = "File is missing.";
                    return BadRequest(response);
                }

                using (var stream = request.File.OpenReadStream())
                {
                    response = _importExcelScoreRepository.AddExcelWithListScore(request, stream, option, _studentRepository);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while uploading Excel file.");
                // Set the response to indicate failure
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
