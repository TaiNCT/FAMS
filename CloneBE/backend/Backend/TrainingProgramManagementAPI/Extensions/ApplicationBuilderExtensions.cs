namespace TrainingProgramManagementAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /*Handle validation exception*/
        public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
        {
            // Add middleware to pipeline will catch exceptions
            app.UseExceptionHandler(x =>
            {
                // Add middleware delegate to application's request pipeline
                x.Run(async context =>
                { 
                    // Get errors features
                    var errorFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeatures?.Error;

                    if (!(exception is FluentValidation.ValidationException validationException))
                        throw exception!;

                    // config to problem details
                    var result = validationException.Errors.ToProblemDetails();

                    // config response
                    var errorContext = JsonSerializer.Serialize(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        IsSuccess = false,
                        Message = validationException.Message,
                        Errors = result.Errors
                    });
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorContext, UTF8Encoding.UTF8);
                });
            });
        }
    }
}