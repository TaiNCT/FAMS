using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAPI.Models;

[Table("RefreshToken")]
public class ApplicationRefreshToken
{
    [Key]
    public Guid Id { get; set; }
    public string RefreshTokenId { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string UserId { get; set; } = string.Empty;


    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;
}