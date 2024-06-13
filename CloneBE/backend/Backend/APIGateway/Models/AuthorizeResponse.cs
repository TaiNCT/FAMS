using Ocelot.Errors;
using Ocelot.Responses;

namespace APIGateway.Models;

public class AuthorizeResponse<T> : Response<T>
{
    public AuthorizeResponse(T data) : base(data)
    {
    }

    public AuthorizeResponse(List<Error> errors) : base(errors)
    {
    }
}