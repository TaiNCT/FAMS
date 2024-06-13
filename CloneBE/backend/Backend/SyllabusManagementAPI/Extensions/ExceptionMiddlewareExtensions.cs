using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.Exceptions.NotFoundException;

namespace SyllabusManagementAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            //app.UseExceptionHandler(appError =>
            //{
            //    appError.Run(async context =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.ContentType = "application/json";

            //        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            //        if (contextFeature != null)
            //        {
            //            // Only modify this code block
            //            // Set the HTTP status code based on the type of exception
            //            context.Response.StatusCode = contextFeature.Error switch
            //            {
            //                NotFoundException => StatusCodes.Status404NotFound,
            //                _ => StatusCodes.Status500InternalServerError
            //            };

            //            logger.LogError($"Something went wrong: {contextFeature.Error}");

            //            await context.Response.WriteAsync(new ResponseDTO()
            //            {
            //                StatusCode = context.Response.StatusCode,
            //                Message = contextFeature.Error.Message,
            //                IsSuccess = false,
            //                Result = new ResultDTO
            //                {
            //                    Data = new List<object>()
            //                }
            //            }.ToString());
            //        }
            //    });
            //});
        }
    }
}