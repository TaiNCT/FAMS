using Ocelot.Errors;

namespace APIGateway.Models;

public class CustomError : Error
{
    public CustomError(string message, OcelotErrorCode code, int httpStatusCode) 
        : base(message, code, httpStatusCode)
    {
    }
}