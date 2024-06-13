using AutoMapper;
using Entities.Models;
using ClassManagementAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ClassManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FSUController : ControllerBase
    {

        private readonly ILogger<FSUController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IFSURepository _fsuService;
        public FSUController(ILogger<FSUController> logger, IElasticClient client, IMapper mapper, IFSURepository fsuService)
        {
            _logger = logger;
            _client = client;
            _fsuService = fsuService;
            _mapper = mapper;
        }

        [HttpGet("GetFSUList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fsu>))]
        public async Task<IActionResult> GetFSUList()
        {
            _logger.LogInformation("Success");
            var items = await _fsuService.GetAllFSU();
            var respone = new
            {
                items
            };
            return Ok(respone);
        }
    }
}
