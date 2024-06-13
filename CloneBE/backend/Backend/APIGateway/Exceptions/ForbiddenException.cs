namespace APIGateway.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Forbidden: Access is denied.")
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}