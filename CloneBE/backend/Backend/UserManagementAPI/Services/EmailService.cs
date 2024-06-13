using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UserManagementAPI.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var fromEmail = emailSettings.GetValue<string>("NoReplyEmail");
            var password = emailSettings.GetValue<string>("EmailPassword");
            var host = emailSettings.GetValue<string>("SmtpServer");
            var port = emailSettings.GetValue<int>("SmtpPort");

            var smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }

}
