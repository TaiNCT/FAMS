using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Nest;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.AssessmentScheme;
using SyllabusManagementAPI.Entities.DTO.Syllabus;
using Entities.Models;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.ServiceContracts;


namespace SyllabusManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyllabusController : ControllerBase
    {
        private readonly IServiceWrapper _service;

        public SyllabusController(IServiceWrapper service)
        {
            _service = service;
        }

        /// <summary>
        /// Represents an action result that performs a HTTP GET operation and returns a response.
        /// </summary>
        /// <param name="syllabusParameters">The parameters for filtering the syllabus.</param>
        /// <returns>An IActionResult object representing the HTTP response.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSyllabus([FromQuery] SyllabusParameters syllabusParameters)
        {
            var syllabus = await _service.SyllabusService.GetAllSyllabusAsync(syllabusParameters);
            return Ok(syllabus);
        }

        /// <summary>
        /// Retrieves a syllabus by its ID.
        /// </summary>
        /// <param name="id">The ID of the syllabus to retrieve.</param>
        /// <returns>An IActionResult object representing the HTTP response.</returns>
        [HttpGet("{id}", Name = "SyllabusById")]
        public async Task<IActionResult> GetSyllabusById(string id)
        {
            var syllabus = await _service.SyllabusService.GetSyllabusByIdAsync(id);
            return Ok(syllabus);
        }

        /// <summary>
        /// Filters syllabus by date range.
        /// </summary>
        /// <param name="syllabusParameters">The parameters for filtering the syllabus.</param>
        /// <param name="from">The start date of the date range.</param>
        /// <param name="to">The end date of the date range.</param>
        /// <returns>An IActionResult object representing the HTTP response.</returns>
        [HttpGet]
        [Route("/date-range")]
        public async Task<IActionResult> FilterByDateSyllabus([FromQuery] SyllabusParameters syllabusParameters, DateTime from, DateTime to)
        {
            var syllabus = await _service.SyllabusService.FilterByDateSyllabusAsync(syllabusParameters, from, to);
            return Ok(syllabus);
        }

        /// <summary>
        /// Sorts syllabus by a specified field.
        /// </summary>
        /// <param name="syllabusParameters">The parameters for sorting the syllabus.</param>
        /// <param name="Sortby">The field to sort by.</param>
        /// <returns>An IActionResult object representing the HTTP response.</returns>
        [HttpGet]
        [Route("/{Sortby}")]
        public async Task<IActionResult> Sort([FromQuery] SyllabusParameters syllabusParameters, string Sortby)
        {
            var syllabus = await _service.SyllabusService.SortSyllabusAsync(syllabusParameters, Sortby);
            return Ok(syllabus);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSyllabus([FromBody] SyllabusForCreationDTO syllabus, bool isDraft = false)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdSyllabus = await _service.SyllabusService.CreateSyllabusAsync(syllabus, isDraft);

            return CreatedAtRoute("SyllabusById", new { id = createdSyllabus.SyllabusId }, createdSyllabus);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSyllabus([FromBody] SyllabusForUpdateDTO syllabus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateSyllabus = await _service.SyllabusService.UpdateSyllabusAsync(syllabus);
            return Ok(updateSyllabus);
        }
        [HttpPost]
        [Route("/duplicate")]
        public async Task<IActionResult> DuplicateProgramAsync([FromBody] DuplicateSyllabusRequest request)
        {
            var duplicatedSyllabus = await _service.SyllabusService.DuplicateSyllabusAsync(request);
            return Ok(duplicatedSyllabus);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSyllabus(string syllabusId)
        {
            await _service.SyllabusService.DeleteSyllabusAsync(syllabusId);

            return NoContent();
        }

        [HttpPatch("/active-deactive")]
        public async Task<IActionResult> ActivateAndDeactivateSyllabus([FromQuery] string syllabusId, [FromQuery] bool activate)
        {
            await _service.SyllabusService.ActivateDeactivateSyllabus(syllabusId, activate);
            return NoContent();
        }

        [HttpPost("/import")]
        public async Task<IActionResult> ImportSyllabus([FromForm] DuplicateHandlingDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.File == null || model.File.Length <= 0)
            {
                throw new ArgumentException("File is empty or missing.");
            }

            if (!Path.GetFileNameWithoutExtension(model.File.FileName).Equals("Template_Import_Syllabus", StringComparison.OrdinalIgnoreCase) ||
                !Path.GetExtension(model.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Please upload right the template: 'Template_Import_Syllabus.xlsx'.");
            }

            var importSyllabus = await _service.SyllabusService.HandleDuplicate(model);

            if (importSyllabus.Item2 == true)
            {
                var importAssesment = await _service.AssessmentSchemeService.ImportAssessmentScheme(importSyllabus.Item1.SyllabusId, model.File);

                var importOutputStandard = await _service.OutputStandardService.ImportOutputStandard(model.File);

                await _service.SyllabusDayService.ImportSyllabusDay(importSyllabus.Item1.SyllabusId, model.File);
            }
            return Ok();
        }

		[HttpGet("/search-query")]
		public async Task<IActionResult> Search([FromQuery] string keywords, [FromQuery] SyllabusParameters syllabusParameters)
		{
			var response = await _service.SyllabusService.SearchAsync(keywords, syllabusParameters);
			return Ok(response);
		}
	}
}
