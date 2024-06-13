using AutoMapper;
using EmailInformAPI.DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Entities.Context;
using Entities.Models;
using Newtonsoft.Json;

namespace EmailInformAPI.Controllers
{

    //[Authorize]
    [Route("api/emailTemplates")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly FamsContext _db;


        public EmailTemplateController(FamsContext db)
        {
            _db = db;
        }


        [HttpGet("{id}")]
        public ActionResult<EmailTemplateDTO> GetById(int id)
        {
            var entity = _db.EmailTemplates.Find(id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = new EmailTemplateDTO
            {
                Name = entity.Name,
                Description = entity.Description,
                IdType = entity.Type,
                Status = entity.Id
            };

            return Ok(dto);
        }

        [HttpPost("add")]
        public async Task<ActionResult<AddEmailTemplateDTO>> AddNew([FromBody] AddEmailTemplateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid input!");
            }

            if (createDto.Type < 1 || createDto.Type > 4)
            {
                return BadRequest("Type must be between 1 and 4.");
            }
            var emt = new EmailTemplate
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Type = createDto.Type,
                IdStatus = 2,
            };

            _db.EmailTemplates.Add(emt);
            await _db.SaveChangesAsync();

            string applyTo = "";

            if (createDto.ApplyTo?.ToLower() == "student")
            {
                applyTo = "student";

                var students = await _db.Students.ToListAsync();

                foreach (var student in students)
                {
                    var emailSend = new EmailSend
                    {
                        TemplateId = emt.EmailTemplateId,
                        Content = JsonConvert.SerializeObject(new
                        {
                            from = "",
                            isactive = false,
                            subject = "",
                            body = "",
                            isdearname = false,
                            quick = false,
                            attendscore = false,
                            isaudit = false,
                            ispracticescore = false,
                            isgpa = false,
                            finalstatus = false

                        }),
                        SendDate = DateTime.Now,
                        ReceiverType = 1,
                    };

                    _db.EmailSends.Add(emailSend);
                    await _db.SaveChangesAsync();

                    var emailSendStudent = new EmailSendStudent
                    {
                        ReceiverId = student.StudentId,
                        EmailId = emailSend.EmailSendId,
                        ReceiverType = 1,
                    };

                    _db.EmailSendStudents.Add(emailSendStudent);
                    await _db.SaveChangesAsync();
                }

            }
            else if (createDto.ApplyTo?.ToLower() == "trainer")
            {
                applyTo = "trainer";

                var trainers = await _db.Users.Where(u => u.Role != null && u.Role.RoleName != null && u.Role.RoleName.ToLower() == "trainer").ToListAsync();

                foreach (var trainer in trainers)
                {
                    var emailSend = new EmailSend
                    {
                        TemplateId = emt.EmailTemplateId,
                        Content = JsonConvert.SerializeObject(new
                        {
                            from = "",
                            isactive = false,
                            subject = "",
                            body = "",
                            isdearname = false,
                            quick = false,
                            attendscore = false,
                            isaudit = false,
                            ispracticescore = false,
                            isgpa = false,
                            finalstatus = false

                        }),
                        SendDate = DateTime.Now,
                        ReceiverType = 2,
                    };

                    _db.EmailSends.Add(emailSend);
                }
                await _db.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Invalid ApplyTo value! It must be 'student' or 'trainer'.");
            }

            var addedDto = new AddEmailTemplateDTO
            {
                Name = emt.Name,
                Description = emt.Description,
                Type = emt.Type,
                ApplyTo = applyTo,
            };
            return CreatedAtAction(nameof(GetById), new { id = emt.Id }, addedDto);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<EmailTemplateDTO>>> GetAll(
    string orderBy = "",
    string sortOrder = "",
    string? status = null,
    string? type = null,
    string? applyTo = null)
        {
            IQueryable<EmailTemplate> query = _db.EmailTemplates;

            if (!string.IsNullOrWhiteSpace(orderBy) &&
                typeof(EmailTemplate).GetProperties().Any(prop => prop.Name.Equals(orderBy, StringComparison.OrdinalIgnoreCase)))
            {
                Expression<Func<EmailTemplate, object>> orderByExpression = orderBy.ToLower() switch
                {
                    "name" => e => e.Name,
                    "description" => e => e.Description,
                    "type" => e => e.Type,
                    "status" => e => e.IdStatus,
                    _ => (Expression<Func<EmailTemplate, object>>)(e => e.Id),
                };

                if (string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(orderByExpression);
                }
                else
                {
                    query = query.OrderBy(orderByExpression);
                }
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                var statusValues = status.Split(',').Select(int.Parse).ToArray();
                query = query.Where(e => statusValues.Contains(e.IdStatus));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                var typeValues = type.Split(',').Select(int.Parse).ToArray();
                query = query.Where(e => typeValues.Contains(e.Type));
            }

            if (!string.IsNullOrWhiteSpace(applyTo))
            {
                var applyToLowerCase = applyTo.ToLower();
                if (applyToLowerCase == "student")
                {
                    query = query.Where(e => e.EmailSends.Any(es =>
                        es.EmailSendStudents.Any(ess =>
                            ess.ReceiverId != null && _db.Students.Any(s => s.StudentId == ess.ReceiverId))));
                }
                else if (applyToLowerCase == "trainer")
                {
                    query = query.Where(e => e.EmailSends.Any() && !e.EmailSends.Any(es => es.EmailSendStudents.Any()));
                }
            }


            query = query.OrderByDescending(e => e.Id);

            var emailTemplates = await query
            .Include(e => e.EmailSends)
            .ThenInclude(es => es.Sender)
            .ThenInclude(u => u.Role)
            .Include(e => e.EmailSends)
            .ThenInclude(es => es.EmailSendStudents)
            .ToListAsync();


            var paginatedDtoList = emailTemplates
                .Select(e => new EmailTemplateDTO
                {
                    TemId = e.EmailTemplateId,
                    Name = e.Name,
                    Description = e.Description,
                    IdType = e.Type,
                    Type = GetTypeDescription(e.Type),
                    Status = e.IdStatus,
                    ApplyTo = e.EmailSends.Any() ?
                        (e.EmailSends.Any(es => es.EmailSendStudents.Any(ess => ess.ReceiverId != null && _db.Students.Any(s => s.StudentId == ess.ReceiverId))) ? "Student" :
                        e.EmailSends.Any(es => es.EmailSendStudents.Any()) ? "" : "Trainer") : "",
                }).ToList();

            return Ok(paginatedDtoList);
        }


        private static string GetTypeDescription(int type)
        {
            return type switch
            {
                1 => "Reserve",
                2 => "Remind",
                3 => "Notice",
                4 => "Other",
            };
        }


        [HttpPut("edit/{id}")]
        public async Task<ActionResult<EmailTemplateDTO>> Edit(int id, [FromBody] EmailTemplateDTO editDto)
        {
            if (id <= 0 || editDto == null)
            {
                return BadRequest("Invalid id or editDto");
            }

            var existingEntity = await _db.EmailTemplates.FindAsync(id);

            if (existingEntity == null)
            {
                return NotFound("Entity not found");
            }

            existingEntity.Name = editDto.Name;
            existingEntity.Description = editDto.Description;
            existingEntity.Type = editDto.IdType;
            existingEntity.IdStatus = editDto.Status;

            await _db.SaveChangesAsync();

            var updatedDto = new EmailTemplateDTO
            {
                Name = existingEntity.Name,
                Description = existingEntity.Description,
                IdType = existingEntity.Type,
                Status = existingEntity.IdStatus

            };

            return Ok(updatedDto);
        }

    }
}
