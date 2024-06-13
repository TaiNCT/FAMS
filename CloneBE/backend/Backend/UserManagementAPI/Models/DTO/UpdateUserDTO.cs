namespace UserManagementAPI.Models.DTO
{
    public class UpdateUserDTO
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public DateTime? Dob { get; set; }

        public string Gender { get; set; }

        public string? RoleId { get; set; }

        public string? RoleName { get; set; }

        public bool Status { get; set; }
    }
}
