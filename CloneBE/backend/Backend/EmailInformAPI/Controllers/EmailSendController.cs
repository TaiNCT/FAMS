using EmailInformAPI.DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Entities.Context;
using Entities.Models;
using Nest;
using Newtonsoft.Json;

namespace EmailInformAPI.Controllers
{
    [Route("api/emailsend")]
    [ApiController]
    public class EmailSendController : ControllerBase
    {
        private readonly FamsContext _db;

        public EmailSendController(FamsContext db)
        {
            _db = db;
        }

        [HttpGet("emailsendlist")]
        public async Task<ActionResult<IEnumerable<EmailSendDTOs>>> GetEmailSendList()
        {
            var emailSend = await _db.EmailSends.ToListAsync();
            var emailSendDTOs = emailSend.Select(et => new EmailSendDTOs
            {
                EmailSendId = et.EmailSendId,
                Id = et.Id,
                TemplateId = et.TemplateId,
                SenderId = et.SenderId,
                FullName = _db.Users.FirstOrDefault
                           (u => u.UserId ==
                                 et.SenderId)?
                                    .FullName,
                Content = et.Content,
                SendDate = et.SendDate,
                ReceiverType = et.ReceiverType,
                Role = GetUserRole(et.SenderId)
            }).ToList();

            return emailSendDTOs;
        }

        private string GetUserRole(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null && user.RoleId != null)
            {
                var role = _db.Roles.FirstOrDefault(r => r.RoleId == user.RoleId);
                if (role != null)
                {
                    return role.RoleName;
                }
            }
            return null;
        }

        //GET: api/emailsend/{id}
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<EmailSendDTOs>> GetEmailSend(int id)
        {
            var emailSend = await _db.EmailSends.FindAsync(id);

            if (emailSend == null)
            {
                return BadRequest("Not Found!");
            }

            var sender = await _db.Users.FirstOrDefaultAsync
                (u => u.UserId ==
                emailSend.SenderId);

            string senderName = sender?.FullName;

            var emailTemplate = await _db.EmailTemplates.FirstOrDefaultAsync
                (et => et.EmailTemplateId ==
                emailSend.TemplateId);

            string tempName = emailTemplate.Name;
            string description = emailTemplate.Description;

            var emailSendDTO = new EmailSendDTOs
            {
                EmailSendId = emailSend.EmailSendId,
                Id = emailSend.Id,
                TemplateId = emailSend.TemplateId,
                SenderId = emailSend.SenderId,
                FullName = senderName,
                Content = emailSend.Content,
                SendDate = emailSend.SendDate,
                ReceiverType = emailSend.ReceiverType,
            };

            return emailSendDTO;
        }

        [HttpGet("preview/{tempId}")]
        public async Task<ActionResult<EmailSendDTOs>> GetEmailbyTempId(string tempId)
        {
            var emailSend = await _db.EmailSends.FirstOrDefaultAsync(es => es.TemplateId == tempId);

            if (emailSend == null)
            {
                return BadRequest("Not Found!");
            }

            var sender = await _db.Users.FirstOrDefaultAsync(u => u.UserId == emailSend.SenderId);

            string senderName = sender?.FullName;

            IQueryable<EmailTemplate> query = _db.EmailTemplates;

            var emailTemplate = await _db.EmailTemplates.Include(e => e.EmailSends)
                .ThenInclude(es => es.Sender)
                .ThenInclude(u => u.Role)
                .Include(e => e.EmailSends)
                .ThenInclude(es => es.EmailSendStudents).FirstOrDefaultAsync(et => et.EmailTemplateId == tempId);

            string tempName = emailTemplate.Name;
            string description = emailTemplate.Description;
            int categories = emailTemplate.Type;
            DateTime createdDate = emailTemplate.CreatedDate;
            string createdDateString = createdDate.ToString();

            // Grabbing the role
            var role = emailTemplate.EmailSends.Any() ?
                        (emailTemplate.EmailSends.Any(es => es.EmailSendStudents.Any(ess => ess.ReceiverId != null && _db.Students.Any(s => s.StudentId == ess.ReceiverId))) ? "Student" :
                        emailTemplate.EmailSends.Any(es => es.EmailSendStudents.Any()) ? "" : "Trainer") : "";

            var emailSendDTO = new EmailSendDTOs
            {
                EmailName = tempName,
                Description = description,
                CreatedDate = createdDate, // Assign the DateTime directly
                EmailSendId = emailSend.EmailSendId,
                Id = emailSend.Id,
                TemplateId = emailSend.TemplateId,
                SenderId = emailSend.SenderId,
                FullName = senderName,
                Content = emailSend.Content,
                SendDate = emailSend.SendDate,
                ReceiverType = categories,
                Role = role,
            };

            return emailSendDTO;
        }


        // POST: api/emailsend/create
        [HttpPost("create")]
        public async Task<ActionResult<EmailSendDTOs>> CreateEmailSend(AddEmailSendDTO createDTO)
        {
            try
            {
                // Kiểm tra xem TemplateId có tồn tại trong bảng EmailTemplates không
                var emailTemplate = await _db.EmailTemplates.FirstOrDefaultAsync(et => et.EmailTemplateId == createDTO.EmailTemplateId);

                if (emailTemplate == null)
                {
                    return BadRequest("Email template not found!");
                }

                // Tạo mới đối tượng EmailSend từ dữ liệu được gửi từ client
                var emailSend = new EmailSend
                {
                    TemplateId = createDTO.EmailTemplateId,
                    SenderId = createDTO.SenderId,
                    Content = createDTO.Content,
                    SendDate = DateTime.Now, // Có thể sử dụng thời gian gửi từ client nếu cần
                    ReceiverType = 0,
                };

                var emailSendJson = JsonConvert.SerializeObject(emailSend);

                // Thêm đối tượng EmailSend vào cơ sở dữ liệu
                _db.EmailSends.Add(emailSend);
                await _db.SaveChangesAsync();

                // Trả về kết quả thành công cùng với thông tin đối tượng EmailSend đã tạo
                return CreatedAtAction(nameof(GetEmailSend), new { id = emailSend.Id }, emailSend);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có bất kỳ lỗi nào xảy ra trong quá trình tạo mới
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/emailsend/edit/{templateId}
        [HttpPost("edit")]
        public async Task<IActionResult> EditEmailSend(EditEmailSendDTO editDTO)
        {

            string templateId = editDTO.templateId;

            var emailSend = await _db.EmailSends.FirstOrDefaultAsync
                            (es => es.TemplateId == templateId);

            if (emailSend == null)
            {
                return NotFound("Email send not found!");
            }

            var emailTemplate = await _db.EmailTemplates.FirstOrDefaultAsync
                                (et => et.EmailTemplateId == templateId);

            if (emailTemplate == null)
            {
                return BadRequest("Email template not found!");
            }

            emailSend.Content = editDTO.Content;
            emailSend.SendDate = DateTime.Now;

            _db.Entry(emailSend).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailSendExists(templateId))
                {
                    return NotFound("Email send not found!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EmailSendExists(string templateId)
        {
            return _db.EmailSends.Any(e => e.TemplateId == templateId);
        }

        // PUT api/emailtemplate/status
        [HttpPut("change-status")]
        public IActionResult UpdateStatus(StatusDTO statusDto)
        {

            var emailTemplate = _db.EmailTemplates.FirstOrDefault
                (e => e.EmailTemplateId == statusDto.EmailTemplateId);

            if (statusDto.Status != 1 && statusDto.Status != 2)
            {
                return BadRequest("Status must be either 1 or 2.");
            }

            if (emailTemplate == null)
            {
                return NotFound("Email template not found.");
            }

            emailTemplate.IdStatus = statusDto.Status;
            _db.SaveChanges();
            return Ok("Status updated successfully.");
        }

    }
}
