using EmailInformAPI.DTO;
using Entities;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace EmailInformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTemplateController : ControllerBase
    {
        private readonly IElasticClient _client;

        public SearchTemplateController(IElasticClient client)
        {
            _client = client;
        }

        [HttpGet(Name = "search")]
        public async Task<IActionResult> Get(string keyword)
        {
            var result = await _client.SearchAsync<EmailTemplate>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                        )
                    ).Size(1000)
                );
            return Ok(result.Documents.ToList());
        }

        [HttpPost(Name = "add")]
        public async Task<IActionResult> Post(AddEmailTemplateDTO email)

        {
            await _client.IndexDocumentAsync(email);
            return Ok();
        }
    }
}
