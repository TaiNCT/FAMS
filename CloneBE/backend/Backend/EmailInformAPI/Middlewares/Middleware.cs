using Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailInformAPI.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public Middleware(RequestDelegate next)
        {
            _next = next;
            _logFilePath = "log.txt";
        }

        public async Task Invoke(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                await _next(context);
                return;
            }

            string claim = authorizationHeader.Split(" ")[1].Split(".")[1].Trim();
            int paddingsize = 4 - (claim.Length % 4);
            string padding = String.Concat(Enumerable.Repeat("=", paddingsize==4 ? 0 : paddingsize));

            string json = Encoding.UTF8.GetString(System.Convert.FromBase64String( claim + padding ));

            JObject ret = JObject.Parse(json);

            string username = ret["username"].ToString();

            if (context.Request.Path.StartsWithSegments("/api/emailTemplates/add"))
            {
                LogToFile($"{username} - Add new email template");
            }
            else if (context.Request.Path.StartsWithSegments("/api/emailsend/edit"))
            {
                LogToFile($"{username} - Edit email template");
            }
            else if (context.Request.Path.StartsWithSegments("/api/Send/schedule/email"))
            {
                LogToFile($"System - Automatic send email to all student");
            }
            else if (context.Request.Path.StartsWithSegments("/api/Send/email"))
            {
                LogToFile($"{username} - Send email");
            }

            await _next(context);
        }

        private void LogToFile(string message)
        {
            int lastNo = 0;

            try
            {
                using (StreamReader reader = new StreamReader(_logFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, @"No: (\d+)");
                        if (match.Success)
                        {
                            int currentNo = int.Parse(match.Groups[1].Value);
                            if (currentNo > lastNo)
                            {
                                lastNo = currentNo;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Log file does not exist, no need to handle this error
            }

            int monthNumber = DateTime.Now.Month;
            DateTime currentDateTime = DateTime.Now;
            DateTime monthDateTime = new(currentDateTime.Year, monthNumber, currentDateTime.Day);

            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine($"No: {lastNo + 1} - {DateTime.Now.ToString($"{monthDateTime.ToString("MMMM")} dd, yyyy HH:mm:ss")} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
