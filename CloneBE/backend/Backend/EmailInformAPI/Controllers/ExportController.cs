using Entities.Context;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly FamsContext _db;

        public ExportController(FamsContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet("emailtemplates")]
        public async Task<IActionResult> ExportEmailTemplatesToExcel()
        {
            var emailTemplates = await _db.EmailTemplates.OrderByDescending(e => e.Id).Include(e => e.EmailSends)
            .ThenInclude(es => es.Sender)
            .ThenInclude(u => u.Role)
            .Include(e => e.EmailSends)
            .ThenInclude(es => es.EmailSendStudents)
            .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("EmailTemplates");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Status";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Category";
                worksheet.Cells[1, 5].Value = "Apply to";
                worksheet.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true; 
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid; 
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aquamarine);
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin; 
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin; 
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin; 
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                int row = 2;
                foreach (var template in emailTemplates)
                {
                    var status = "";

                    var category = "";

                    if (template.IdStatus != null)
                    {
                        status = template.IdStatus == 1 ? "Active" : (template.IdStatus == 2 ? "Inactive" : "");
                    }

                    string applyTo = template.EmailSends.Any() ?
             (template.EmailSends.Any(es => es.EmailSendStudents.Any(ess => ess.ReceiverId != null && _db.Students.Any(s => s.StudentId == ess.ReceiverId))) ? "Student" :
             template.EmailSends.Any(es => es.EmailSendStudents.Any()) ? "" : "Trainer") : "";

                    var applyToLowerCase = applyTo.ToLower();
                    // Cập nhật giá trị "Trainer" cho cột ApplyTo nếu là trường hợp Trainer
                    if (applyTo == "" && applyToLowerCase == "trainer")
                    {
                        applyTo = "Trainer";
                    }

                    if (template.EmailSends.Any())
                    {

                        category = template.Type switch
                        {
                            1 => "Inform",
                            2 => "Remind",
                            3 => "Notice",
                            4 => "Other",
                        };
                    }
                    worksheet.Cells[row, 1].Value = template.Name;
                    worksheet.Cells[row, 2].Value = status;
                    worksheet.Cells[row, 3].Value = template.Description;
                    worksheet.Cells[row, 4].Value = category;
                    worksheet.Cells[row, 5].Value = applyTo;
                    row++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                using (var range = worksheet.Cells[worksheet.Dimension.Address])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                }
                // Lưu file Excel
                byte[] excelData = package.GetAsByteArray();
                string fileName = $"EmailTemplates_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var fileStream = new MemoryStream(excelData);
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
