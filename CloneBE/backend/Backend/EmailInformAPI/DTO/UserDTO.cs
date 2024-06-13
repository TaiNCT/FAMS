using Entities;

namespace EmailInformAPI.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Address { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? RoleId { get; set; }
        public bool Status { get; set; }
        public virtual RoleDTO? Role { get; set; }

    }
}
