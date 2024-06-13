namespace APIGateway.Models.Constants;

public class RequestMethods
{
    private static Dictionary<string, string> MethodActionMap = new Dictionary<string, string>()
    {
        { "GET", "VIEW" },
        { "POST", "CREATE" },
        { "PUT", "MODIFY" }
    };

    public static bool GetActionForMethod(string method, out string action)
    {
        if (MethodActionMap.TryGetValue(method, out action!))
        {
            return true;
        }

        return false;
    }
}

