using AutoMapper;
using ClassManagementAPI.Models;
using ClassManagementAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ClassManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeTypeController : ControllerBase
    {

        private readonly ILogger<AttendeeTypeController> _logger;
        private readonly IElasticClient _client;
        private readonly IMapper _mapper;
        private readonly IAttendeeTypeRepository _attendeetype;
        public AttendeeTypeController(ILogger<AttendeeTypeController> logger, IElasticClient client, IMapper mapper, IAttendeeTypeRepository attendeetype)
        {
            _logger = logger;
            _client = client;
            _attendeetype = attendeetype;
            _mapper = mapper;
        }

        [HttpGet("GetAttendeeTypeList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fsu>))]
        public async Task<IActionResult> GetAttendeeTypeList()
        {
            _logger.LogInformation("Success");
            var attendeeTypes = await _attendeetype.GetAllAttendeeTypeList();
            var respone = new
            {
                attendeeTypes
            };
            return Ok(respone);
        }
    }
}
