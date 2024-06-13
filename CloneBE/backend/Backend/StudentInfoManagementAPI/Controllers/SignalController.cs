using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentInfoManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        [HttpGet("/logout/{uuid}")]
        public async Task<IActionResult> LogoutAll(string uuid)
        {
            return Ok(1);
        }
    }
}
