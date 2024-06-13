using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Repository;

namespace ScoreManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {


        private readonly IScoreRepository _scoreRepository;
        public ExportController(IScoreRepository scoreRepository) {
            _scoreRepository = scoreRepository;
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(string id) {

            var scores = _scoreRepository.GetScoresByClassId(id);
            if (!scores.Any())
            {
                return NotFound("No scores found for the specified class.");
            }

            // Create the Excel workbook
            using (var workbook = new XLWorkbook()){

                // Add a worksheet
                var worksheet = workbook.Worksheets.Add("Result");

                // Write some data to the worksheet
                worksheet.Cell("A1").Value = "Name";
                worksheet.Cell("B1").Value = "Account";
                worksheet.Cell("C1").Value = "Type";
                var quizRow = worksheet.Range("D1:J1");
                quizRow.Merge();
                quizRow.Value = "Quiz";

                var asmrow = worksheet.Range("K1:N1");
                asmrow.Merge();
                asmrow.Value = "ASM";

                worksheet.Cell("O1").Value = "Quiz Final";
                worksheet.Cell("P1").Value = "Audit";
                worksheet.Cell("Q1").Value = "Practice Final";
                worksheet.Cell("R1").Value = "GPA Module";
                worksheet.Cell("S1").Value = "Level Module";
                worksheet.Cell("T1").Value = "MOCK";
                worksheet.Cell("U1").Value = "GPA Module";
                worksheet.Cell("V1").Value = "Level Module";
                worksheet.Cell("W1").Value = "Status";

                // Add stuffs into 2nd row
                worksheet.Cell("C2").Value = "Subject";
                worksheet.Cell("D2").Value = "HTML";
                worksheet.Cell("E2").Value = "CSS";
                worksheet.Cell("F2").Value = "Quiz 3";
                worksheet.Cell("G2").Value = "Quiz 4";
                worksheet.Cell("H2").Value = "Quiz 5";
                worksheet.Cell("I2").Value = "Quiz 6";
                worksheet.Cell("J2").Value = "Average";
                worksheet.Cell("K2").Value = "Practice 1";
                worksheet.Cell("L2").Value = "Practice 2";
                worksheet.Cell("M2").Value = "Practice 3";
                worksheet.Cell("N2").Value = "Average";

                // Adding score into the sheet
                int rowIndex = 3;
                foreach(ScoreDTO record in scores){

                    double ?avequiz = null;
                    double ?aveasm = null;

                    if(record.html!=null && record.css!=null && record.quiz3 !=null && record.quiz4 != null && record.quiz5!=null && record.quiz6 != null)
                    {
                        avequiz = (record.html + record.css + record.quiz3 + record.quiz4 + record.quiz5 + record.quiz6) / 6;
                    }
                    if (record.practice1 != null && record.practice2 != null && record.practice3 != null)
                    {
                        aveasm = (record.practice3 + record.practice2 + record.practice1) / 3;
                    }


                    worksheet.Cell($"A{rowIndex}").Value = record.FullName;
                    worksheet.Cell($"B{rowIndex}").Value = record.Faaccount;
                    worksheet.Cell($"C{rowIndex}").Value = "";
                    worksheet.Cell($"D{rowIndex}").Value = record.html;
                    worksheet.Cell($"E{rowIndex}").Value = record.css;
                    worksheet.Cell($"F{rowIndex}").Value = record.quiz3;
                    worksheet.Cell($"G{rowIndex}").Value = record.quiz4;
                    worksheet.Cell($"H{rowIndex}").Value = record.quiz5;
                    worksheet.Cell($"I{rowIndex}").Value = record.quiz6;
                    worksheet.Cell($"J{rowIndex}").Value = avequiz!=null ? ((double)avequiz).ToString("0.00") : null; // Average score of all quizes
                    worksheet.Cell($"K{rowIndex}").Value = record.practice1;
                    worksheet.Cell($"L{rowIndex}").Value = record.practice2;
                    worksheet.Cell($"M{rowIndex}").Value = record.practice3;
                    worksheet.Cell($"N{rowIndex}").Value = aveasm!=null ? ((double)aveasm).ToString("0.00") : null; // Average score of all practices
                    worksheet.Cell($"O{rowIndex}").Value = record.quizfinal;
                    worksheet.Cell($"P{rowIndex}").Value = record.audit;
                    worksheet.Cell($"Q{rowIndex}").Value = record.pracfinal;
                    worksheet.Cell($"R{rowIndex}").Value = record.gpa1; // Final Module
                    worksheet.Cell($"S{rowIndex}").Value = record.level1;
                    worksheet.Cell($"T{rowIndex}").Value = record.mock;
                    worksheet.Cell($"U{rowIndex}").Value = record.gpa2; // Status
                    worksheet.Cell($"V{rowIndex}").Value = record.level2;
                    worksheet.Cell($"W{rowIndex}").Value = record.gpa2 > 60 ? "Passed" : "Failed"; // Final Module
/*                    worksheet.Cell($"X{rowIndex}").Value = ;
                    worksheet.Cell($"Y{rowIndex}").Value = ;
                    worksheet.Cell($"Z{rowIndex}").Value = ;*/

                    rowIndex++;
                }

                worksheet.Column("A").Width = 20; 
                worksheet.Column("B").Width = 20; 
                worksheet.Column("Q").Width = 20;
                worksheet.Column("O").Width = 20;
                worksheet.Column("R").Width = 20;
                worksheet.Column("S").Width = 20;
                worksheet.Column("Q").Width = 20;
                worksheet.Column("V").Width = 20;
                worksheet.Column("K").Width = 20;
                worksheet.Column("K").Width = 20;
                worksheet.Column("L").Width = 20;
                worksheet.Column("M").Width = 20;
                worksheet.Column("N").Width = 20;

                worksheet.Row(1).Height = 30;
                worksheet.Row(2).Height = 30;

                // Align them at the center
                worksheet.Rows().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Rows().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                // Create a memory stream to store the Excel file
                using (var stream = new MemoryStream())
                {
                    // Save the workbook to the memory stream
                    workbook.SaveAs(stream);

                    // Set the position of the stream back to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // Set the content type and file name for the response
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = "example.xlsx";

                    // Return the Excel file as a file content result
                    var fileContentResult = new FileContentResult(stream.ToArray(), contentType)
                    {
                        FileDownloadName = fileName
                    };

                    return fileContentResult;
                }
            }

        }
    }
}
