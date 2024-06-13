namespace UserManagementAPI.Models.DTO
{
    public class EmailSettings
    {
        public string NoReplyEmail { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }

}
