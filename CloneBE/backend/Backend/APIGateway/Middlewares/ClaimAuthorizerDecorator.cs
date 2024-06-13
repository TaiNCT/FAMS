using System.Net;
using System.Security.Claims;
using APIGateway.Models;
using APIGateway.Models.Constants;
using APIGateway.Utils;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Authorization;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Errors;
using Ocelot.Responses;

namespace APIGateway.Middleware;

public class ClaimAuthorizerDecorator : IClaimsAuthorizer
{
    private const string ROLE_CLAIMNAME = "Role";
    private const string FUNCTION_CLAIMNAME = "Function";
    private const string FULLACCESS = "FULL ACCESS";
    private const string ACCESSDENIED = "ACCESS DENIED";

    private readonly ClaimsAuthorizer _authorizer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private List<Claim> UserClaims = null!;

    public ClaimAuthorizerDecorator(ClaimsAuthorizer authorizer,
        IHttpContextAccessor httpContextAccessor,
        TokenValidationParameters tokenValidationParameters)
    {
        _authorizer = authorizer;
        _httpContextAccessor = httpContextAccessor;
        _tokenValidationParameters = tokenValidationParameters;
    }

    //  Summary:
    //      Implement Authorize function from IClaimsAuthorizer, to Authorize user based on route requirement claims
    public Response<bool> Authorize(ClaimsPrincipal claimsPrincipal,
                                    Dictionary<string, string> routeClaimsRequirement,
                                    List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)
    {
        // Process if not authenticated even exist Bearer Token (Development env)
        // This's bug issues related to have multiple Authentication Scheme 
        // like JWT Authentication, Cookie Authentication running together.
        // [User.Identity.IsAuthenticated](https://stackoverflow.com/questions/70996969/user-identity-isauthenticated-is-false-in-a-non-auth-methods-after-successful-lo)
        if (!claimsPrincipal.Identity?.IsAuthenticated ?? false)
        {
            Task.Run(async () =>
            {
                await ProcessAuthenticateAsync();
            }).GetAwaiter().GetResult();
        }
        else // With Docker env, authenticate run successfully
        {
            // Get all claims
            UserClaims = claimsPrincipal.Claims.ToList();
        }

        // Claim identity
        var claimsIdentity = new ClaimsIdentity(UserClaims, JwtBearerDefaults.AuthenticationScheme);

        // Create a ClaimsPrincipal using the ClaimsIdentity
        claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Process new RouteClaimsRequirement in case of exist semicolon inside config file
        var newRouteClaimsRequirement = new Dictionary<string, string>();
        // Flag: default role claims require is not found
        var isClaimRoleValidObj = new { isRoleMatch = false, roleName = "" };
        // Flag: Is not Valid Action method
        // var isValidActionMethod = false;

        foreach (var kvp in routeClaimsRequirement)
        {
            // Custom When IdentityServer exist claims type (ClaimTypes.Role)
            // appsettings cannot defined semicolon so we replace http:// to http///
            // in RouteClaimsRequirement in ocelot custom
            if (kvp.Key.StartsWith("http///"))
            {
                var key = kvp.Key.Replace("http///", "http://");
                newRouteClaimsRequirement.Add(key, kvp.Value);
            }
            else if (kvp.Key.StartsWith(ROLE_CLAIMNAME) && kvp.Value.Contains(","))
            {
                // Split the role values separated by comma
                var roles = kvp.Value.Split(",").Select(v => v.Trim()).ToList();

                // Authorize each role individually
                foreach (var role in roles)
                {
                    var roleClaims = new Dictionary<string, string>
                    {
                        { kvp.Key, role }
                    };

                    var authorizeResult = _authorizer.Authorize(
                        claimsPrincipal, roleClaims, urlPathPlaceholderNameAndValues);

                    if (!authorizeResult.IsError)
                    {
                        isClaimRoleValidObj = new { isRoleMatch = true, roleName = role };
                    }
                }
            }
            else
            {
                newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
            }
        }

        if (isClaimRoleValidObj.isRoleMatch)
        {
            HttpContext ctx = _httpContextAccessor.HttpContext!;

            // Create a scope from the HttpContext's request services
            using (var scope = ctx.RequestServices.CreateScope())
            {
                // Resolve the FamsContext within the scope
                var dbContext = scope.ServiceProvider.GetRequiredService<FamsContext>();

                // Get request method
                var requestMethod = ctx.Request.Method.ToUpper();

                // Get role by name
                var userRole = dbContext.Roles
                    .Include(x => x.RolePermission)
                    .First(x => !string.IsNullOrEmpty(x.RoleName) &&
                        x.RoleName.ToUpper().Equals(isClaimRoleValidObj.roleName.ToUpper()));

                // Get function name from route claims require
                if (routeClaimsRequirement.TryGetValue(FUNCTION_CLAIMNAME, out var functionClaimName))
                {

                    // Get action value base on method name
                    RequestMethods.GetActionForMethod(requestMethod, out var actionMethod);

                    if (userRole.RolePermission != null &&
                        IsValidAction(dbContext, userRole.RolePermission, functionClaimName, actionMethod))
                    {
                        // isValidActionMethod = true;
                        newRouteClaimsRequirement.Add(ROLE_CLAIMNAME, isClaimRoleValidObj.roleName);
                        newRouteClaimsRequirement.Remove(FUNCTION_CLAIMNAME);
                    }
                }
            }
        }
        else
        {
            return _authorizer.Authorize(claimsPrincipal,
                new Dictionary<string, string>() { { "FORBID_CLAIM", "NOT_ALLOW_TO_ACCESS" } },
                urlPathPlaceholderNameAndValues);

            // var errorList = new List<Error>()
            // {
            //    new CustomError(
            //             "NOT_ALLOW_TO_ACCESS",
            //             OcelotErrorCode.UnauthorizedError,
            //             StatusCodes.Status403Forbidden)
            // };

            // return new AuthorizeResponse<object>(errorList);
        }

        // We can check more claims here to facilitate role authorization based on each application function...
        // Define later in RouteClaimRequirement...

        return _authorizer.Authorize(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
    }

    public async Task ProcessAuthenticateAsync()
    {
        HttpContext ctx = _httpContextAccessor.HttpContext!;

        // Check exist JWT Bearer token in request header 
        var customAuthService = ctx.RequestServices.GetRequiredService<CustomAuthenticationMiddleware>();
        if (!await customAuthService.IsExistAuthorizationBearer(ctx)) // Not exist
        {
            // Process Unauthorized(401)
            await ReturnStatus(ctx, HttpStatusCode.Unauthorized, "You don't have permission to access");
            return;
        }

        // Get access token
        var token = ctx.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // Initiate Json Web token handler
        var tokenHandler = new JsonWebTokenHandler();

        // Get service scope IAuthenticateService, which is declared in Program.cs (AddAuthentication().AddJwtBearer())
        var authenticationService = ctx.RequestServices.GetRequiredService<IAuthenticationService>();
        try
        {
            // Validate the token using TokenValidationParameters
            var principal = await tokenHandler.ValidateTokenAsync(token, _tokenValidationParameters);

            // If token validation succeeds, proceed with JWT authentication 
            var authenticateResult = await authenticationService.AuthenticateAsync(ctx, JwtBearerDefaults.AuthenticationScheme);

            // Succeeded -> Get all Claims
            if (authenticateResult.Succeeded) UserClaims = authenticateResult.Principal.Claims.ToList();
        }
        catch (SecurityTokenValidationException)
        {
            // Token validation failed, handle the exception appropriately
            ctx.Response.StatusCode = 401; // Unauthorized
            return;
        }
    }

    private static bool IsValidAction(FamsContext context,
        RolePermission rolePermission,
        string functionName,
        string actionName)
    {
        var rolePermissionId = string.Empty;
        switch (functionName.ToUpper())
        {
            case ApplicationFunctions.TRAININGPROGRAM:
                rolePermissionId = rolePermission.TrainingProgram;
                break;
            case ApplicationFunctions.SYLLABUS:
                rolePermissionId = rolePermission.Syllabus;
                break;
            case ApplicationFunctions.CLASS:
                rolePermissionId = rolePermission.Class;
                break;
            case ApplicationFunctions.STUDENT:
                rolePermissionId = rolePermission.Class;
                break;
            case ApplicationFunctions.SCORE:
                rolePermissionId = rolePermission.Class;
                break;
            case ApplicationFunctions.RESERVATION:
                rolePermissionId = rolePermission.Class;
                break;
            case ApplicationFunctions.USERMANAGEMENT:
                rolePermissionId = rolePermission.UserManagement;
                break;
            case ApplicationFunctions.LEARNINGMATERIAL:
                rolePermissionId = rolePermission.LearningMaterial;
                break;
            case ApplicationFunctions.EMAIL:
                return true;
            default:
                return false;
        }

        // Process authorize permission for each function 
        return ProcessUserPermission.IsAllowCrossApplication(context, rolePermissionId, actionName, FULLACCESS, ACCESSDENIED);
    }

    private static async Task ReturnStatus(HttpContext context, HttpStatusCode statusCode, string msg)
    {
        // Response status code 
        context.Response.StatusCode = (int)statusCode;
        // Response body
        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = (int)statusCode,
            Message = msg
        });
        // Complete response
        await context.Response.CompleteAsync();
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection DecorateClaimAuthoriser(this IServiceCollection services)
    {
        // Get first service descriptor typeof(IClaimsAuthorizer)
        var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(IClaimsAuthorizer));
        if (serviceDescriptor != null)
        {
            // Remove existing 
            services.Remove(serviceDescriptor);

            // Add transient service (invoke while request)
            services.AddTransient(serviceDescriptor.ImplementationType!, serviceDescriptor.ImplementationType!);
            services.AddTransient<IClaimsAuthorizer, ClaimAuthorizerDecorator>();
        }

        return services;
    }
}