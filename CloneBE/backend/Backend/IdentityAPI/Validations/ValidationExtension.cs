namespace IdentityAPI.Validations;

public static class ValidationExtension
{
    public static ValidationProblemDetails ToProblemDetails(this FluentValidation.Results.ValidationResult result)
    {
        // Init ValidationProblemDetails
        var error = new ValidationProblemDetails { Status = StatusCodes.Status400BadRequest };

        // Each error in ValidationResult.Errors is ValidationFailure -> (Property, ErrorMessage)   
        foreach (var validationFailure in result.Errors)
        {
            // If error property already exist 
            if (error.Errors.ContainsKey(validationFailure.PropertyName))
            {
                // From key -> get valudate and concat with new errors arr
                error.Errors[validationFailure.PropertyName] =
                    error.Errors[validationFailure.PropertyName]
                        .Concat(new[] { validationFailure.ErrorMessage }).ToArray();
            }
            else // not exist property
            {
                error.Errors.Add(new KeyValuePair<string, string[]>(
                    validationFailure.PropertyName,
                    new [] {validationFailure.ErrorMessage}));
            }
        }
        
        return error;
    }
}