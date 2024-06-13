namespace IdentityAPI.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public DateTime Dob { get; set; }
    public string Address { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? RoleId { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? Avatar { get; set; }
    public bool Status { get; set; }
}
