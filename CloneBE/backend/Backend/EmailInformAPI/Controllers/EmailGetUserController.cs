using EmailInformAPI.DTO;
using EmailInformAPI.Repository;
using EmailInformAPI.Scheduler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailInformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailGetUserController : ControllerBase
    {
        private readonly IEmailRepository _repository;

        private readonly IConfiguration _configuration;

        private readonly IScheduler _scheduler;


        // Email information
        private readonly string email;
        private readonly string emailtoken;

        public EmailGetUserController(IEmailRepository repository, IConfiguration configuration, IScheduler scheduler)
        {

            // Import the repositories and scopes
            _configuration = configuration;
            _repository = repository;
            _scheduler = scheduler;

            // Grabbing the email and token information
            email = _configuration.GetValue<string>("ConnectionStrings:NoReplyEmail");
            emailtoken = _configuration.GetValue<string>("ConnectionStrings:EmailKey");
        }

        [HttpPost("get/{name}")]
        public IEnumerable<UserGetDTO> GetUser(string name)
        {
            return _repository.GetUser(name);               
        }
    }
}
