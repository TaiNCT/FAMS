using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Azure.Core;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityAPI.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;



namespace IdentityAPI.Services;

public class CustomRefreshTokenService : IRefreshTokenService
{
    // Expiration time to seconds
    private const int RefreshTokenExpirationTime = 432000;

    private readonly UserManager<ApplicationUser> _userMananger;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContext;
    private readonly ApplicationDbContext _context;
    private readonly Microsoft.IdentityModel.Tokens.TokenValidationParameters _tokenValidationParameters;
    private readonly AppSettings _appSettings;

    // Properties
    public string Username { get; set; } = string.Empty;
    public string RefreshTokenId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;

    // Constants
    const string ScopeClaimType = "scope";
    const string IdpClaimType = "idp";
    const string UserNameClaimType = "username";
    const string AuthTimeClaimType = "auth_time";
    const string SubjectClaimType = "sub";
    const string AmrClaimType = "amr";
    // Application allowed scopes
    public string[] scopes = { "profile", "openid", "FamsApp", "offline_access" };
    // Amrs 
    public string[] amrs = { "pwd" };

    public CustomRefreshTokenService(UserManager<ApplicationUser> userMananger,
        IWebHostEnvironment env,
        IHttpContextAccessor httpContext,
        IOptionsMonitor<AppSettings> monitor,
        ApplicationDbContext context,
        Microsoft.IdentityModel.Tokens.TokenValidationParameters tokenValidationParameters)
    {
        _userMananger = userMananger;
        _env = env;
        _httpContext = httpContext;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
        _appSettings = monitor.CurrentValue;
    }

    /// <summary>
    ///     Generate Refresh Token comes up with Access Token while get access permission 
    /// <summary>
    public async Task<string> CreateRefreshTokenAsync(RefreshTokenCreationRequest request)
    {
        // handle request to access props
        await HandleRequestAsync();

        // Check exist username
        var applicationUser = await _userMananger.FindByNameAsync(Username);
        // Not allowed to access web application
        if (applicationUser is null) return null!;

        // Generate refresh token id 
        var refreshTokenId = Guid.NewGuid().ToString();

        // Save refresh token with application user 
        var creationDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"),
             "yyyy-MM-dd", CultureInfo.InvariantCulture);
        var expirationDate = creationDate.AddSeconds(RefreshTokenExpirationTime);
        // Generate application refresh token 
        var refreshToken = new ApplicationRefreshToken
        {
            Id = Guid.NewGuid(),
            RefreshTokenId = refreshTokenId,
            CreationDate = creationDate,
            ExpiryDate = expirationDate,
            UserId = applicationUser.Id,
            User = applicationUser
        };


        /// Assump that this function invoke by user already login to application
        /// Update their prev refresh token instead of creating new one

        // Check exist user and refresh token 
        var existedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x =>
            x.UserId == applicationUser.Id);
        if (existedRefreshToken is not null)
        {
            // Update existing refresh token 
            existedRefreshToken.RefreshTokenId = refreshToken.RefreshTokenId;
            existedRefreshToken.CreationDate = refreshToken.CreationDate;
            existedRefreshToken.ExpiryDate = refreshToken.ExpiryDate;

            // Mark the entity as modified
            _context.RefreshTokens.Update(existedRefreshToken);
        }
        else
        {
            // Save refresh token to DB
            await _context.AddAsync(refreshToken);
        }

        var result = await _context.SaveChangesAsync() > 0;

        return result ? refreshTokenId : null!;
    }

    /// <summary>
    ///     Update Referesh Token
    /// <summary>
    public async Task<string> UpdateRefreshTokenAsync(RefreshTokenUpdateRequest request)
    {
        // handle request to access props
        await HandleRequestAsync();

        // Check exist username
        var applicationUser = await _userMananger.FindByNameAsync(Username);
        // Not allowed to access web application
        if (applicationUser is null) return null!;

        // Generate refresh token id 
        var refreshTokenId = Guid.NewGuid().ToString();

        // Save refresh token with application user 
        var creationDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"),
             "yyyy-MM-dd", CultureInfo.InvariantCulture);
        var expirationDate = creationDate.AddSeconds(RefreshTokenExpirationTime);

        var existedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x =>
            x.UserId == applicationUser.Id
        && x.RefreshTokenId == RefreshTokenId);
        if (existedRefreshToken is not null)
        {
            // Update existing refresh token 
            existedRefreshToken.RefreshTokenId = refreshTokenId;
            existedRefreshToken.CreationDate = creationDate;
            existedRefreshToken.ExpiryDate = expirationDate;

            // Mark the entity as modified
            _context.RefreshTokens.Update(existedRefreshToken);
        }

        var result = await _context.SaveChangesAsync() > 0;

        return result ? refreshTokenId : null!;
    }

    /// <summary>
    ///     Validate Referesh Token 
    /// </summary>
    public async Task<TokenValidationResult> ValidateRefreshTokenAsync(string tokenHandle, Client client)
    {
        // Handle request to get appropriate props
        await HandleRequestAsync();

        // Check valid access token 
        var result = await ValidateAccessTokenAsync(AccessToken);
        if (!result.Item1) return result.Item2;

        // Check exist username
        var applicationUser = await _userMananger.FindByNameAsync(Username);
        // Not allowed to access web application
        if (applicationUser is null) return new TokenValidationResult
        {
            IsError = true,
            ErrorDescription = $"Not found any username match {Username}"
        };

        // Check exist refresh token 
        var existRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.RefreshTokenId == RefreshTokenId);
        if (existRefreshToken is null) return new TokenValidationResult
        {
            IsError = true,
            ErrorDescription = $"Wrong Refresh Token"
        };

        var validatedRequest = new ValidatedRequest();
        validatedRequest.SetClient(client);

        var validatedResources = new ResourceValidationResult();
        validatedResources.Filter(scopes);

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
            new Claim(UserNameClaimType, Username),
            new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString())
        });
        // Add amr claims
        var armClaims = amrs.Select(method => new Claim(AmrClaimType, method)).ToArray();
        claimsIdentity.AddClaims(armClaims);

        // Create a ClaimsPrincipal with the claims identity
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Generate refresh token 
        var refreshToken = new RefreshToken
        {
            CreationTime = DateTime.UtcNow,
            ConsumedTime = null, // Consumed time, set to null initially
            Subject = claimsPrincipal, // Subject associated with the token
            Version = 5, // Version number
            ClientId = client.ClientId,
            SessionId = null, // Session identifier, set to null initially
            AuthorizedScopes = scopes,
            AuthorizedResourceIndicators = null, // Authorized resource indicators, set to null initially
            ProofType = null // Proof type, set to null initially
        };

        // Generate access token 
        var accessToken = new JwtHelper(_env, _appSettings).GenerateToken(client, Username, scopes, armClaims);

        // Set access token to refresh token 
        refreshToken.SetAccessToken(accessToken);

        return new TokenValidationResult()
        {
            IsError = false,
            RefreshToken = refreshToken
        };
    }

    private async Task HandleRequestAsync()
    {
        var httpRequest = _httpContext.HttpContext?.Request;

        // Check if request is of type 'application/x-www-form-urlencoded'        
        if (httpRequest?.ContentType != "application/x-www-form-urlencoded")
            return;

        // Access data
        StringValues values = default;
        // Check exist username in request body
        var existUsername = httpRequest?.Form.TryGetValue("username", out values);
        if (existUsername.HasValue && existUsername.Value)
        {
            // Assign to username props 
            Username = values.FirstOrDefault() ?? "";
        }
        // Check exist refresh token in request body
        var existRefreshToken = httpRequest?.Form.TryGetValue("refresh_token", out values);
        if (existRefreshToken.HasValue && existRefreshToken.Value)
        {
            // Assign to refresh token props
            RefreshTokenId = values.FirstOrDefault() ?? "";
        }
        // Check exist access token in request body
        var existAccessToken = httpRequest?.Form.TryGetValue("access_token", out values);
        if (existAccessToken.HasValue && existAccessToken.Value)
        {
            // Assign to access token props
            AccessToken = values.FirstOrDefault() ?? "";
        }

        await Task.CompletedTask;
    }

    private async Task HandleErrorResponseAsync(int statusCode, string error, string? errorDescription)
    {
        if (_httpContext.HttpContext is null) return;

        var response = new TokenValidationResult
        {
            IsError = true,
            Error = error,
            ErrorDescription = errorDescription
        };

        // Response status
        _httpContext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        // Serialize custom response to JSON 
        var jsonErrorResponse = JsonSerializer.Serialize(response);

        // Set response content type
        _httpContext.HttpContext.Response.ContentType = "application/json";

        // Write JSON response to response body 
        await _httpContext.HttpContext.Response.WriteAsync(jsonErrorResponse);

        return;
    }

    //private async Task<(bool, TokenValidationResult)> ValidateAccessTokenAsync(string accessToken)
    //{
    //    // Check exist access token
    //    if (string.IsNullOrEmpty(accessToken))
    //        return (false, new TokenValidationResult { IsError = true });

    //    var tokenHandler = new JwtSecurityTokenHandler();

    //    try
    //    {
    //        // Get token pricipals
    //        var pricipal = tokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
    //            out var _);

    //        // Not found required pricipals
    //        if (pricipal is null)
    //        {
    //            // await HandleErrorResponseAsync(400, "Invalid Token", "Invalid Token");
    //            return (false, new TokenValidationResult
    //            {
    //                IsError = true,
    //                Error = "invalid_token",
    //                ErrorDescription = "Invalid Token"
    //            });
    //        };

    //        // Get unix time from token 
    //        var expiryDateUnix =
    //            long.Parse(pricipal.Claims.Single(x => x.Type == JwtClaimTypes.Expiration).Value);
    //        // Convert unix time to UTC DateTime
    //        var expiryUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

    //        if (expiryUtc < DateTime.UtcNow)
    //        {
    //            // await HandleErrorResponseAsync(400, "Expiry", "Token hasn't expired yet");
    //            return (false, new TokenValidationResult
    //            {
    //                IsError = true,
    //                Error = "expiry",
    //                ErrorDescription = "Token hasn't expired yet"
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        return (false, new TokenValidationResult { IsError = true });
    //    }
    //    finally
    //    {
    //        await Task.CompletedTask;
    //    }

    //    return (true, null!);
    //}

    private async Task<(bool, TokenValidationResult)> ValidateAccessTokenAsync(string accessToken)
    {
        // Check exist access token
        if (string.IsNullOrEmpty(accessToken))
            return (false, new TokenValidationResult { IsError = true });


        var tokenHandler = new JsonWebTokenHandler();
        // var test = new JsonWebTokenHandler();

        try
        {
            // Get token pricipals
            var pricipal = await tokenHandler.ValidateTokenAsync(accessToken, _tokenValidationParameters);

            // Not found required pricipals
            if (pricipal is null)
            {
                // await HandleErrorResponseAsync(400, "Invalid Token", "Invalid Token");
                return (false, new TokenValidationResult
                {
                    IsError = true,
                    Error = "invalid_token",
                    ErrorDescription = "Invalid Token"
                });
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
                        return (false, new TokenValidationResult
                        {
                            IsError = true,
                            Error = "expiry",
                            ErrorDescription = "Token hasn't expired yet"
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (false, new TokenValidationResult { IsError = true });
        }
        finally
        {
            await Task.CompletedTask;
        }

        return (true, null!);
    }

    private bool IsJWTWithValidSecurityAlthorithm(Token accessToken)
    {
        return accessToken is Token token
            && token.AllowedSigningAlgorithms.Contains(
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256);
    }
}
