namespace UserManagementAPI.Models.DTO
{
    public class UpdateInfoDTO
    {
        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime? Dob { get; set; }

        public string Gender { get; set; } = string.Empty;


    }
}
