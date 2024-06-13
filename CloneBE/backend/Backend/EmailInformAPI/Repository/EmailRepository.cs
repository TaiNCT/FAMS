using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Entities.Models;
using Entities.Context;
using EmailInformAPI.DTO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;

namespace EmailInformAPI.Repository
{
    public class EmailRepository : IEmailRepository
    {


        private readonly FamsContext _context;
        private string emailcontent;
        private string scorecontent;
        private string pwd = Path.Combine(Directory.GetCurrentDirectory(), "Pages");

        public EmailRepository(FamsContext context)
        {
            _context = context;

            // Grabbing the content of the templates and place it into the variables
            emailcontent = File.ReadAllText(Path.Combine(pwd, "email.html"));
            scorecontent = File.ReadAllText(Path.Combine(pwd, "score.html"));
        }

        public IEnumerable<UserGetDTO> GetUser(string name)
        {
            // _context.UserPermissions
            string name_ = name.Trim().ToLower();

            if (name_.Equals("trainer"))
            {
                return _context.Users.Where(s => s.Role.RoleName.Equals(name)).Select(e => new UserGetDTO
                {
                    email = e.Email,
                    username = e.FullName,
                    id = e.UserId
                }).ToList();
            }

            return _context.Students.Select(e => new UserGetDTO
            {
                email = e.Email,
                username = e.FullName,
                id = e.StudentId
            }).ToList();
        }
        /*   Testing
        {
            "lastmsg": "<label>WHat the fuck am i doing with my life</label>",
            "firstmsg": "<h1>Son of bitches</h1>",
            "subject": "Fuck me, God dammit !!!",
            "body": {
            "html": 23,
            "css": 23,
            "quiz3": 91,
            "quiz4": 28,
            "quiz5": 83,
            "quiz6": 10,
            "quiz_ave": 23,
            "practice1": 54,
            "practice2": 100,
            "practice3": 19,
            "asm_ave": 98,
            "quizfinal": 18,
            "audit": 23,
            "practicefinal": 92,
            "status": true,
            "mock": 23,
            "final": 23,
            "gpa": 49,
            "level": 19
            },
            "title": "Fuck me please",
            "recipient": "email@gmail.com"
        }*/
        public bool SendEmail(string recipient, string subject, string firstmsg, string lastmsg, string title, DTO.Score scores, string email, string emailtoken, ExtraOption? options)
        {


            // Create a new email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email, title, System.Text.Encoding.UTF8);
            mail.To.Add(new MailAddress(recipient));
            mail.Subject = subject;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            // Checking the body of the HTML whether to send score or just simply text

            string body = emailcontent.Replace("CONTENT_SECTION", $"{firstmsg}<br>{lastmsg}");

            if (scores != null)
            {
                // Send scores
                mail.Body = body.Replace("SCORE_SECTION", scorecontent.
                Replace("{HTML}", scores.html.ToString()).
                Replace("{CSS}", scores.css.ToString()).
                Replace("{Quiz3}", scores.quiz3.ToString()).
                Replace("{Quiz4}", scores.quiz4.ToString()).
                Replace("{Quiz5}", scores.quiz5.ToString()).
                Replace("{Quiz6}", scores.quiz6.ToString()).
                Replace("{QuizAve}", scores.quiz_ave.ToString()).
                Replace("{Practice1}", scores.practice1.ToString()).
                Replace("{Practice2}", scores.practice2.ToString()).
                Replace("{Practice4}", scores.practice3.ToString()).
                Replace("{AsmAve}", scores.asm_ave.ToString()).
                Replace("{MOCK}", scores.mock.ToString()).
                Replace("{QuizFinal}", scores.quizfinal.ToString()).
                Replace("{PracticeFinal}", scores.practicefinal.ToString()));

                if (options != null)
                {
                    // Check some options before proceeding
                    if (options.isaudit == true)
                        mail.Body = mail.Body.Replace("{Audit}", scores.audit.ToString());
                    else
                        mail.Body = mail.Body.Replace("{Audit}", "").Replace("Audit", "");

                    if (options.isgpa == true)
                    {
                        mail.Body = mail.Body.Replace("{GPA}", scores.gpa.ToString());
                    }
                    else
                    {
                        mail.Body = mail.Body.Replace("{GPA}", "").Replace("GPA", "");
                    }

                    if (options.isfinalstatus == true)
                    {
                        mail.Body = mail.Body.Replace("{Status}", (bool)scores.status ? "Passed" : "Failed");
                    }
                    else
                    {
                        mail.Body = mail.Body.Replace("{Status}", "").Replace("Status", "");
                    }
                }
                else
                    mail.Body = mail.Body.Replace("{Audit}", scores.audit.ToString())
                        .Replace("{GPA}", scores.gpa.ToString())
                        .Replace("{Status}", (bool)scores.status ? "Passed" : "Failed");
            }
            else
                mail.Body = body.Replace("SCORE_SECTION", "");

            // Trimming the body
            mail.Body = mail.Body.Trim();

            // Sending the email
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential(email, emailtoken);
                client.EnableSsl = true;
                //  Send the email
                client.Send(mail);
            }


            return true;
        }
    }
}
