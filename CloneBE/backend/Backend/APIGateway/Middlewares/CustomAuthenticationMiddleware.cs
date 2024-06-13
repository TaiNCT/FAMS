public class CustomAuthenticationMiddleware
{
    public async Task<bool> IsExistAuthorizationBearer(HttpContext context)
    {
        string authorizationHeader = context.Request.Headers["Authorization"]!;
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            // If the header is missing or invalid, authentication fails
            await Task.CompletedTask;
            return false;
        }
        else
        {
            // If the header is present and valid, authentication succeeds
            await Task.CompletedTask;
            return true;
        }
    }
}