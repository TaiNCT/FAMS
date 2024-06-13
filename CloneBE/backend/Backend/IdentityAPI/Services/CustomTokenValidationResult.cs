using Duende.IdentityServer.Validation;
using ValidationResult = Duende.IdentityServer.Validation.ValidationResult;

namespace IdentityAPI.Services;
public class CustomTokenValidationResult : TokenValidationResult
{
    public CustomTokenValidationResult(bool isError, string error, string? errorDescription):base()
    {
        IsError = isError;
        Error = error;
        ErrorDescription = errorDescription;
    }
}