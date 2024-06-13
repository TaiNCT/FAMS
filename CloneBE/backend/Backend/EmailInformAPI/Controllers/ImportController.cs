using EmailInformAPI.DTO;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Linq;

namespace EmailInformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly FamsContext _db;

        public ImportController(FamsContext dbContext)
        {
            _db = dbContext;
        }

        [HttpPost("emailtemplates/import")]
        public async Task<IActionResult> ImportEmailTemplatesFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet == null)
                    {
                        return BadRequest("The Excel file does not contain any worksheets.");
                    }

                    var rowCount = worksheet.Dimension?.Rows;

                    if (rowCount == null || rowCount < 2)
                    {
                        return BadRequest("The worksheet does not contain enough rows.");
                    }

                    var importedTemplates = new List<EmailTemplateDTO>();

                    int row = 2;

                    while (true)
                    {
                        var template = new EmailTemplateDTO
                        {
                            Name = worksheet.Cells[row, 1].Value?.ToString(),
                            Status = MapStatusToInt(worksheet.Cells[row, 2].Value?.ToString()),
                            Description = worksheet.Cells[row, 3].Value?.ToString(),
                            IdType = MapTypeToInt(worksheet.Cells[row, 4].Value?.ToString()),
                            ApplyTo = worksheet.Cells[row, 5].Value?.ToString()
                        };

                        if (string.IsNullOrEmpty(template.Name) && string.IsNullOrEmpty(template.Description) &&
                            template.Status == 0 && template.IdType == 0 && string.IsNullOrEmpty(template.ApplyTo))
                        {
                            break;
                        }

                        if (!IsValidEmailTemplate(template))
                        {
                            return BadRequest("Invalid email template data found in Excel.");
                        }
                        importedTemplates.Add(template);

                        row++;
                    }

                    var newEmailTemplates = new List<EmailTemplate>();
                    foreach (var importModel in importedTemplates)
                    {
                        var emt = new EmailTemplate
                        {
                            Name = importModel.Name,
                            Description = importModel.Description,
                            Type = importModel.IdType,
                            IdStatus = importModel.Status,
                        };

                        _db.EmailTemplates.Add(emt);
                        await _db.SaveChangesAsync();

                        string applyTo = "";
                        var emailTemplateId = emt.EmailTemplateId;

                        if (importModel.ApplyTo != null && importModel.ApplyTo.ToLower() == "student")
                        {
                            applyTo = "student";

                            var students = await _db.Students.ToListAsync();

                            foreach (var student in students)
                            {
                                var emailSend = new EmailSend
                                {
                                    TemplateId = emailTemplateId,
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

                                if (emailSend.EmailSendId != null)
                                {
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
                        }
                        else if (importModel.ApplyTo != null && importModel.ApplyTo.ToLower() == "trainer")
                        {
                            applyTo = "trainer";

                            var trainers = await _db.Users.Where(u => u.Role != null && u.Role.RoleName != null && u.Role.RoleName.ToLower() == "trainer").ToListAsync();

                            foreach (var trainer in trainers)
                            {
                                var emailSend = new EmailSend
                                {
                                    TemplateId = emailTemplateId,
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
                        }
                        else
                        {
                            return BadRequest("Invalid ApplyTo value! It must be 'student' or 'trainer'.");
                        }
                    }
                    await _db.SaveChangesAsync();
                }
            }
            return Ok("Data imported successfully.");
        }


        private int MapStatusToInt(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return 0;
            }

            switch (status.ToLower())
            {
                case "active":
                    return 1;
                case "inactive":
                    return 2;
                default:
                    return 0;
            }
        }

        private int MapTypeToInt(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return 0;
            }
            switch (type.ToLower())
            {
                case "reserve":
                    return 1;
                case "remind":
                    return 2;
                case "notice":
                    return 3;
                case "other":
                    return 4;
                default:
                    return 0;
            }
        }
        private bool IsValidEmailTemplate(EmailTemplateDTO template)
        {
            // Kiểm tra các điều kiện để xác định dữ liệu từ mẫu email có hợp lệ không
            if (string.IsNullOrEmpty(template.Name) || string.IsNullOrEmpty(template.Description) ||
                template.Status == 0 || template.IdType == 0 || string.IsNullOrEmpty(template.ApplyTo))
            {
                return false;
            }

            if (template.ApplyTo.ToLower() != "student" && template.ApplyTo.ToLower() != "trainer")
            {
                return false;
            }

            return true;


        }
    }
}
