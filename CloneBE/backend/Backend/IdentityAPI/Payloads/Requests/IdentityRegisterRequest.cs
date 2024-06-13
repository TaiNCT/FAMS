namespace IdentityAPI.Payloads.Requests;

public class IdentityRegisterRequest
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public DateTime Dob { get; set; }

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? RoleId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; } = null!;

    public string? Avatar { get; set; }

    [Required]
    public bool Status { get; set; }
}

public static class IdentityRegisterRequestExtension
{
    public static ApplicationUser ToApplicationUser(this IdentityRegisterRequest reqObj, IMapper mapper)
    {
        return mapper.Map<ApplicationUser>(reqObj);
    }
}