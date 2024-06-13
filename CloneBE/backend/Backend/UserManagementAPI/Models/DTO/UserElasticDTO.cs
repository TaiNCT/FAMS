namespace UserManagementAPI.Models.DTO
{
    public class UserElasticDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
}
