

using System.Net;
using System.Security.Claims;
using System.Text;
using APIGateway.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Middleware;

namespace APIGateway.Middleware
{
    //  Summary: 
    //      This custom authorizaton middleware is use for more security context
    //      However, its cannot process response when occured 401, 404 or 403
    //      So, Apply ClaimAuthorizerDecorator for authorization RouteClaimsRequirement
    public class CustomAuthorizationMiddleware : OcelotPipelineConfiguration
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private List<Claim> UserClaims = null!;

        public CustomAuthorizationMiddleware(
            TokenValidationParameters tokenValidationParameters)
        {
            _tokenValidationParameters = tokenValidationParameters;

            // Process Authentication Middleware
            AuthenticationMiddleware = async (ctx, next) =>
            {
                var customAuthService = ctx.RequestServices.GetRequiredService<CustomAuthenticationMiddleware>();
                if (!await customAuthService.IsExistAuthorizationBearer(ctx))
                {
                    await ReturnStatus(ctx, HttpStatusCode.Unauthorized, "You don't have permission to access");
                    return;
                }

                var token = ctx.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var tokenHandler = new JsonWebTokenHandler();
                var authenticationService = ctx.RequestServices.GetRequiredService<IAuthenticationService>();
                try
                {
                    // Validate the token using TokenValidationParameters
                    var principal = await tokenHandler.ValidateTokenAsync(token, _tokenValidationParameters);

                    // If token validation succeeds, proceed with JWT authentication or other logic
                    var authenticateResult = await authenticationService.AuthenticateAsync(ctx, JwtBearerDefaults.AuthenticationScheme);

                    if (authenticateResult.Succeeded) UserClaims = authenticateResult.Principal.Claims.ToList();

                    await next.Invoke();
                }
                catch (SecurityTokenValidationException)
                {
                    // Token validation failed, handle the exception appropriately
                    ctx.Response.StatusCode = 401; // Unauthorized
                    return;
                }
            };

            // Process Authorization Middleware 
            AuthorizationMiddleware = async (ctx, next) =>
            {
                var customAuthService = ctx.RequestServices.GetRequiredService<CustomAuthenticationMiddleware>();
                if (!await customAuthService.IsExistAuthorizationBearer(ctx))
                {
                    return;
                }

                await ProcessRequest(ctx, next);
            };
        }

        public async Task ProcessRequest(HttpContext context, Func<Task> next)
        {
            var routeClaimsRequirement = context.Items.DownstreamRoute().RouteClaimsRequirement;

            routeClaimsRequirement.TryGetValue("Role", out var requiredRole);

            if (string.IsNullOrEmpty(requiredRole))
            {
                await next.Invoke();
                return;
            }

            var claimRole = UserClaims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (!string.IsNullOrEmpty(claimRole) && requiredRole.Equals(claimRole))
            {
                await next.Invoke();
                return;
            }

            // If the user does not have the required role, return a 403 Forbidden response
            // SetErrorResponseOnContext(context, StatusCodes.Status403Forbidden);
            await ReturnStatus(context, HttpStatusCode.Forbidden, "Not allowed");
        }

        private static async Task ReturnStatus(HttpContext context, HttpStatusCode statusCode, string msg)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(msg);
            await context.Response.CompleteAsync();
        }

        public void SetErrorResponseOnContext(HttpContext context, int statusCode)
        {
            context.Response.OnStarting(x =>
                { context.Response.StatusCode = statusCode; return Task.CompletedTask; },
                context
            );
        }
    }
}
