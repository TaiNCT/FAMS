namespace UserManagementAPI.Models.DTO
{
    public class RoleDTO
    {
        public string RoleId { get; set; } = null!;

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? RoleName { get; set; }
    }
}
