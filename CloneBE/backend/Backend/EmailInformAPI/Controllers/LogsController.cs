using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly string _logFilePath;

        public LogController()
        {
            _logFilePath = "log.txt"; 
        }

        [HttpGet]
        public IActionResult GetLogEntries()
        {
            List<LogEntry> logEntries = ReadLogEntries();
            return Ok(logEntries);
        }

        private List<LogEntry> ReadLogEntries()
        {
            List<LogEntry> logEntries = new List<LogEntry>();

            // Đọc nội dung của tệp log.txt
            string[] lines = System.IO.File.ReadAllLines(_logFilePath);

            // Phân tích từng dòng và tạo các đối tượng LogEntry tương ứng
            foreach (string line in lines)
            {
                // Phân tích dòng thành các thành phần: No, Datetime, Modified By, và Action
                string[] parts = line.Split(" - ");
                if (parts.Length == 4)
                {
                    logEntries.Add(new LogEntry
                    {
                        No = int.Parse(parts[0].Split(":")[1].Trim()),
                        DateTime = parts[1].Trim(),
                        ModifiedBy = parts[2].Trim(),
                        Action = parts[3].Trim()
                    });
                }
            }
            return logEntries;
        }
    }

    public class LogEntry
    {
        public int No { get; set; }
        public string DateTime { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
    }
}
