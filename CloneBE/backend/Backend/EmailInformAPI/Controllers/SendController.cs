using EmailInformAPI.DTO;
using EmailInformAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using EmailInformAPI.Scheduler;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmailInformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IEmailRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IScheduler _scheduler;

        // Email information
        private readonly string email;
        private readonly string emailtoken;

        public SendController(IEmailRepository repository, IConfiguration configuration, IScheduler scheduler)
        {

            // Import the repositories and scopes
            _configuration = configuration;
            _repository = repository;
            _scheduler = scheduler;

            // Grabbing the email and token information
            email = _configuration.GetValue<string>("ConnectionStrings:NoReplyEmail");
            emailtoken = _configuration.GetValue<string>("ConnectionStrings:EmailKey");
        }

        [HttpPost("schedule/email")]
        public IActionResult SetSchedulerEmail(EmailScheduleDTO data)
        {

            // Testing the Scheduler
            _scheduler.onTime(data.date, new TimerCallback(state =>
            {
                System.Diagnostics.Debug.WriteLine($"Email sent to \"{data.recipient}\"");
                _repository.SendEmail(data.recipient, data.subject, data.firstmsg, data.lastmsg, data.title, data.body, email, emailtoken, data.options);
            }));

            // Return the response
            return Ok(new
            {
                msg = $"Set a schedule for email \"{data.recipient}\" on {data.date}"
            });
        }

        [HttpPost("email")]
        public IActionResult SendEmail(EmailDTO data)
        {

            _repository.SendEmail(data.recipient, data.subject, data.firstmsg, data.lastmsg, data.title, data.body, email, emailtoken, data.options);

            return Ok(new
            {
                msg = "Successfully sent email."
            });
        }
    }
}
