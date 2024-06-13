using System.Security.Authentication;
using System.Text;
using APIGateway.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionMiddlewareHandler
{
    public static void UseExceptionMiddlewareHandler(this IApplicationBuilder app)
    {
        // Add the ExceptionHandlerMiddleware to the pipeline
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    case ForbiddenException forbiddenException:
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync(forbiddenException.Message, Encoding.UTF8);
                        break;
                    case AuthenticationException authenticationException:
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(authenticationException.Message, Encoding.UTF8);
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("An unexpected error occurred.", Encoding.UTF8);
                        break;
                }
            });
        });
    }
}