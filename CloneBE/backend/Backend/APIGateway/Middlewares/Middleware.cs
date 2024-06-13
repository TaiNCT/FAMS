using APIGateway.Singletons;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Responses;
using System.Text;
using System.Text.Json;

namespace APIGateway.Middleware;

public class Middleware
{
    private readonly RequestDelegate _next;

    private static readonly HttpClient client = new HttpClient();

    private Store _store;

    public Middleware(RequestDelegate next, Store store)
    {
        _next = next;
        _store = store;
    }

    public async Task Invoke(HttpContext context)
    {

        //if (_store.noAuthroutes.Contains(context.Request.Path))
        //{
        //    // This route does not need authorization or authentication, simply move on
        //    await _next(context);
        //    return;
        //}

        /*
                // ------------------- The rest below requires authentication -------------------
                var token = context.Request.Headers.SingleOrDefault(sc => sc.Key.Equals("Authorization")).Value;
                var refreshtoken = context.Request.Headers.SingleOrDefault(sc => sc.Key.Equals("RefreshToken")).Value;


                if (token.IsNullOrEmpty() || refreshtoken.IsNullOrEmpty())
                {
                    // Missing token or refresh token in the header

                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        msg = "Unauthorized",
                        code="MISS_FIELD"
                    }));
                    return;
                }

                // Token found, time to grab the claims
                string claim = token.ToString().Split(" ")[1].Split(".")[1].Trim();
                string json = Encoding.UTF8.GetString(System.Convert.FromBase64String(claim + "="));
                JObject ret = JObject.Parse(json);

                // Crafting a JSON request
                var data = System.Text.Json.JsonSerializer.Serialize(new
                {
                    token = token,
                    userName = ret["username"].ToString(),
                    refreshTokenId = refreshtoken
                });

                // Convert to StringContent object
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                // Send the request
                HttpResponseMessage resp = await client.PostAsync("http://localhost:5000/api/identity/validate-token", content);
                if (resp.IsSuccessStatusCode)
                {
                    string responseContent = await resp.Content.ReadAsStringAsync();
                    if (responseContent.Equals("false"))
                    {
                        // Either username is deleted or expired
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            msg = "Session is no longer valid, please login again.",
                            code = "INV_SESSION"
                        }));
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = Int32.Parse(resp.StatusCode.ToString());
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        msg = resp.Content,
                        code = "ERR"
                    }));
                    Console.WriteLine("POST request failed with status code: " + resp.StatusCode);
                    return;
                }*/

        // This means success
        await _next(context);
    }
}
