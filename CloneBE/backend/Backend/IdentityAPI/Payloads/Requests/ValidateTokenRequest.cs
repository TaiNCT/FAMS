namespace IdentityAPI.Payloads.Requests;

public class ValidateTokenRequest
{
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty; 
    public string RefreshTokenId { get; set; } = string.Empty;  
}