/*using AutoMapper;
using ClassManagementAPI.Models;
using ClassManagementAPI.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ClassManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationService;
        public LocationController(ILogger<LocationController> logger, IElasticClient client, IMapper mapper, ILocationRepository locationService)
        {
            _logger = logger;
            _client = client;
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet("GetLocationList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        public async Task<IActionResult> GetLocationList()
        {
            _logger.LogInformation("Success");
            var items = await _locationService.GetAllLocation();
            var respone = new
            {
                items
            };
            return Ok(respone);
        }
    }
}
*/