using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class RecoverPasswordDTO
{
    // Recover password properties 
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

}