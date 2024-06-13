namespace UserManagementAPI.Models.DTO
{
    public class RecoverCodeDTO
    {
        public string Email {  get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
