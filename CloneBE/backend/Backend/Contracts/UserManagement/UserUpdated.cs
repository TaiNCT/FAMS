namespace Contracts.UserManagement;

public class UserUpdated
{
    // Update properties 
    // Update all user properties to prevent update single field in Consumer
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime Dob { get; set; }
    public string Address { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? RoleId { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; } = null!;
    public string? Avatar { get; set; }
    public bool Status { get; set; }
}