using Microsoft.AspNetCore.Mvc;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Parameters;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchSyllabusListController : ControllerBase
    {
        //private readonly IElasticClient _client;
        private readonly IServiceWrapper _service;

        public SearchSyllabusListController(IServiceWrapper service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("/search")]
        public async Task<IActionResult> Get([FromQuery] string[] keywords, [FromQuery] SyllabusParameters syllabusParameters)
        {
            //_logger.LogInformation("Successfully");
            //var searchResults = await _client.SearchAsync<Syllabus>(
            //    s => s.Query(q =>
            //        q.Bool(b =>
            //            b.Should(
            //                keywords?.Select(keyword =>
            //                    q.QueryString(
            //                        d => d.Query(keyword)
            //                    )
            //                ).ToArray()
            //            )
            //        )
            //    )
            //    .From(syllabusParameters.PageNumber - 1)
            //    .Size(syllabusParameters.PageSize)
            //);

            var searchResults = await _service.elasticService.SearchAsync(keywords, syllabusParameters);

            //var metadata = new
            //{
            //    TotalItems = searchResults.Total,
            //    pageSize = syllabusParameters.PageSize,
            //    CurrentPage = syllabusParameters.PageNumber,
            //    TotalPages = (int)Math.Ceiling((double)searchResults.Total / syllabusParameters.PageSize),
            //    HasNext = searchResults.Total > (syllabusParameters.PageNumber + 1) * syllabusParameters.PageSize,
            //    HasPrevious = syllabusParameters.PageNumber > 1
            //};

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            //// Use LogInformation instead of LogInfo
            //_logger.LogInformation($"Returned syllabus for keywords '{string.Join(", ", keywords)}' from Elasticsearch.");
            //_response.StatusCode = StatusCodes.Status200OK;
            //_response.Title = "Success";
            //_response.Result = searchResults.Documents.ToList();
            return Ok(searchResults);
        }

        [HttpPost(Name = "Create syllabus")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSyllabus(SyllabusDTO model)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        _logger.LogWarning("Fail: " + ModelState);
            //        return BadRequest(ModelState);
            //    }

            //    try
            //    {
            //        _logger.LogInformation("Start Create");
            //        var syllabusModel = _mapper.Map<Syllabus>(model);
            //        var result = await _syllabusService.CreateSyllabus(syllabusModel);
            //        if (result)
            //        {
            //            await _client.IndexDocumentAsync(model);
            //            _logger.LogInformation("Successful");
            //            return Ok("Add Successfully");
            //        }
            //        else
            //        {
            //            _logger.LogError("Fail to create");
            //            ModelState.AddModelError("", "Something wrong while creating syllabus!!!");
            //            return BadRequest(ModelState);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError("Fail");
            //        return BadRequest(ex.Message);
            //    }
            return Ok();
        }
    }
}
