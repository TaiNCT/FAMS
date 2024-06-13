namespace IdentityAPI.Payloads;

public static class APIRoutes
{
    public const string Base = "api";


    public class Identity
    {
        // Default access token: POST /connect/token
        // References: https://docs.duendesoftware.com/identityserver/v7/tokens/requesting/

        // Register 
        public const string Register = Base + "/identity/";

        // Validate Access Token
        public const string ValidateToken = Base + "/identity/validate-token";
    }
}