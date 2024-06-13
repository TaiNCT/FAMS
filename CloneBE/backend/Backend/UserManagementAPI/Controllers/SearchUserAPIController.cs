using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Nest;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class SearchUserAPIController : ControllerBase
    {
        private readonly ILogger<SearchUserAPIController> _logger;
        private readonly IElasticClient _client;


        public SearchUserAPIController(ILogger<SearchUserAPIController> logger, IElasticClient client)
        {
            _logger = logger;
            _client = client;
        }


        [HttpGet("search-user")]
        public async Task<IActionResult> Search(string keyword, int pageNumber = 1, int pageSize = 10, string sortField = "id", string sortOrder = "asc")
        {
            _logger.LogInformation("Search initiated");

            var sortFieldMappings = new Dictionary<string, string>
            {
                { "id", "userId.keyword"},
                { "name", "fullName.keyword"},
                { "email", "email.keyword"},
                { "dob", "dob"},
                { "gender", "gender.keyword"},
                { "type", "status"}
            };

            if (sortFieldMappings.ContainsKey(sortField))
            {
                sortField = sortFieldMappings[sortField];
            }

            var results = await _client.SearchAsync<User>(
                s => s.Query(
                        q => q.Bool(
                            b => b.Must(
                                    m => m.QueryString(
                                        d => d.Query("*" + keyword + "*")
                                    )
                                )
                                .MustNot(
                                    mn => mn.Term(t => t.Field("roleName.keyword").Value("Super Admin")) // Exclude documents where roleName is Super Admin
                                )
                        )
                    )
                    .Sort(sort => sort
                        .Field(sortField, sortOrder.ToLower() == "asc" ? SortOrder.Ascending : SortOrder.Descending)
                    )
                    .From((pageNumber - 1) * pageSize) // Calculate the start index of the page
                    .Size(pageSize) // Size of the page
                );
            var totalCount = results.Total;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Calculate the total number of pages
            var users = results.Documents.ToList();
            var response = new { totalCount, totalPages, users }; // Create a new object containing totalCount, totalPages, and users
            var result = new ResponseDTO();
            result.Result = response;
            return Ok(result);

        }

    }
}
