using Duende.IdentityServer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace IdentityAPI.Utils;

public class JwtHelper
{
    private readonly IWebHostEnvironment _env;
    private readonly AppSettings _appSettings;

    public JwtHelper(IWebHostEnvironment env,
        AppSettings appSettings)
    {
        _env = env;
        _appSettings = appSettings;
    }

    // Constants
    const string ScopeClaimType = "scope";
    const string IdpClaimType = "idp";
    const string UserNameClaimType = "username";
    const string AuthTimeClaimType = "auth_time";
    const string SubjectClaimType = "sub";

    public Token GenerateToken(Client client, string username, string[] scopes, Claim[] amrClaims)
    {
        // Parse string to TimeSpan
        var timeSpan = TimeSpan.Parse(_appSettings.TokenLifeTime.ToString());
        // Access total token life time seconds
        var tokenLifeTime = timeSpan.TotalSeconds;

        // Creation date, current
        var creationTime = DateTime.UtcNow;
        // Expiration date, current + tokenLifeTime seconds
        var expirationTime = creationTime.AddSeconds(tokenLifeTime);

        // Claims
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(AuthTimeClaimType,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer),
            new Claim(SubjectClaimType, Guid.NewGuid().ToString()),
            new Claim(ScopeClaimType, scopes[0]),
            new Claim(ScopeClaimType, scopes[1]),
            new Claim(ScopeClaimType, scopes[2]),
            new Claim(ScopeClaimType, scopes[3]),
            new Claim(IdpClaimType, "local"),
            new Claim(UserNameClaimType, username),
            new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString())
        });
        // Add amr claims
        claimsIdentity.AddClaims(amrClaims);

        // Generate Access Token 
        var token = new Token
        {
            Issuer = _env.IsEnvironment("Development") ? "http://localhost:5000" : "http://identity-svc",
            CreationTime = DateTime.UtcNow,
            Lifetime = (int)(expirationTime - creationTime).TotalSeconds,
            ClientId = client.ClientId,
            AccessTokenType = AccessTokenType.Jwt,
            Claims = claimsIdentity.Claims.ToArray()
        };

        return token;
    }

    public async Task<bool> ValidateAccessTokenAsync(string accessToken, 
        Microsoft.IdentityModel.Tokens.TokenValidationParameters tokenValidationParameters)
    {
        // Check exist access token
        if (string.IsNullOrEmpty(accessToken))
            return false;


        var tokenHandler = new JsonWebTokenHandler();
        // var test = new JsonWebTokenHandler();

        try
        {
            // Get token pricipals
            var pricipal = await tokenHandler.ValidateTokenAsync(accessToken, tokenValidationParameters);

            // Not found required pricipals
            if (pricipal is null)
            {
                // await HandleErrorResponseAsync(400, "Invalid Token", "Invalid Token");
                return false;
            };

            // Get unix time from token 
            if (pricipal.Claims.TryGetValue(JwtClaimTypes.Expiration, out var expiryDateObject))
            {
                if (long.TryParse(expiryDateObject.ToString(), out var expiryDateUnix))
                {
                    // Convert unit time to UTC Datetime
                    var expiryUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

                    if (expiryUtc < DateTime.UtcNow)
                    {
                        // await HandleErrorResponseAsync(400, "Expiry", "Token hasn't expired yet");
                       return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        finally
        {
            await Task.CompletedTask;
        }

        return true;
    }
}