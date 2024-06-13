using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityAPI.Utils;
using Microsoft.Extensions.Options;

namespace IdentityAPI;

public static class Config
{

    const string FAMS_APP = "FamsApp";
    const string OFFLINE_ACCESS = "offline_access";
    const string REFRESH_TOKEN_GRANT_TYPE = "refresh_token";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            // Default ISources (openid, profile) - must be defined in request header
            // whilst get access token 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            // new ApiScope("scope1"),
            // new ApiScope("scope2"),
            new ApiScope("FamsApp", "Fams application full access")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // Custom IdentityServer Clients for Postman and FE
            new Client
            {

                ClientId = "postman",
                ClientSecrets = {new Secret("NotASecret".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword, REFRESH_TOKEN_GRANT_TYPE},

                // Where to redirect to after login (custom back to homepage URL later)
                RedirectUris = {"htts://getpostman.com/oauth2/callback"},

                // Where to redirect to after logout
                PostLogoutRedirectUris = { /*Logout URI here*/ },

                // Enable support for refresh token by setting AllowOfflineAccess flag
                AllowOfflineAccess = true,

                // Theses scopes are required while get access token 
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    FAMS_APP,
                    OFFLINE_ACCESS
                },
                // Set 2 days expired time 
                AccessTokenLifetime = 3600*24*2, // Must be read from configuration instead of hard code.
                // The refresh token will expire on a fixed point in time 
                RefreshTokenExpiration = TokenExpiration.Absolute,
                // The refresh token handle will be updated when refresh token 
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            },
            new Client
            {
                ClientId = "webApp",
                ClientName = "webApp",
                ClientSecrets = {new Secret("FE_fams".Sha256())},
                RequirePkce = false,
                RedirectUris = {"http://localhost:5173/callback/"},
                // Enable support for refresh token by setting AllowOfflineAccess flag
                AllowOfflineAccess = true, 
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword, REFRESH_TOKEN_GRANT_TYPE},
                // Set 2 days expired time 
                AccessTokenLifetime = 3600*24*2, //
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    FAMS_APP,
                    OFFLINE_ACCESS
                },

                // Where to redirect to after logout
                PostLogoutRedirectUris = { /*Logout URI here*/ },
                // The refresh token will expire on a fixed point in time 
                RefreshTokenExpiration = TokenExpiration.Absolute,
                // The refresh token handle will be updated when refresh token 
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            }
        };
}
