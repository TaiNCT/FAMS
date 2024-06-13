using APIGateway.Middleware;
using APIGateway.Singletons;
using Entities.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Ocelot.Authentication.Middleware;
using Ocelot.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);

// ---- Checking environment variables to handle some functionalities
bool isdevelop = builder.Environment.IsDevelopment();
string pwd = Directory.GetCurrentDirectory();
string noAuthFile = Path.Combine(pwd, "Properties", "noAuth.json");
string routes_path = Path.Combine(pwd, isdevelop ? "development" : "production");
Console.WriteLine(isdevelop ? "/* Using development mode" : "/* Running in Production mode.");


// -------- Configuring the builder --------
builder.Configuration
    .SetBasePath(pwd)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddOcelot(routes_path, builder.Environment);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Register your custom authentication service
builder.Services.AddScoped<CustomAuthenticationMiddleware>();


Console.WriteLine("URL {0}", builder.Configuration["IdentityServiceUrl"]);

// Token validaton parameters
var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateAudience = false,
    NameClaimType = "username",
    ValidateLifetime = true,
    ValidateIssuerSigningKey = false,

    // Validate Issuer
    ValidIssuer = builder.Configuration["IdentityServiceUrl"],
    // Set the SignatureValidator to a delegate that always returns true
    SignatureValidator = (token, parameters) =>
    {
        var jwt = new JsonWebToken(token);
        return jwt;
    },
    RequireSignedTokens = false
};

builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        // Config Authority validation
        if (isdevelop) options.Authority = "http://localhost:5000";
        else options.Authority = builder.Configuration["IdentityServiceUrl"];

        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = tokenValidationParameters;
    });
// builder.Services.AddAuthorization();


// Retrieve connnection string
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
// Determine the environment
var isdev = builder.Environment.IsDevelopment();
// Add DbContext
if (isdev)
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(options =>
    {
        options.UseSqlServer(conn);
    }, ServiceLifetime.Scoped);

    builder.Services.AddScoped<FamsContext>();
}
else
{
    conn = Environment.GetEnvironmentVariable("ConnectionString");

    Console.WriteLine("[INFO]: Running in production mode.");

    Console.WriteLine(conn);
    // Use SQL Server for production
    builder.Services.AddDbContext<FamsContext>(options => options.UseSqlServer(conn));
}

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddScoped<ClaimsAuthorizer>();
builder.Services.AddTransient<IClaimsAuthorizer, ClaimsAuthorizer>();
builder.Services.DecorateClaimAuthoriser();

// Adding a singleton to notify the Middleware of routes that requires authentication
//string jsoncontent = File.ReadAllText(noAuthFile).Trim();
//JObject data = JObject.Parse(jsoncontent);
//JToken token = data["Routes"];
//List<string> routes = new List<string>();

//if (token.Type == JTokenType.Array)
//    foreach (JToken child in token.Children())
//        routes.Add(child.ToString());


builder.Services.AddSingleton<Store>(new Store
{
    IsDevelopment = isdevelop,
    //noAuthroutes = routes
});

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// ----- Create an application -----
var app = builder.Build();
// app.UseExceptionMiddlewareHandler();
app.UseRouting();
app.UseCors("Cors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<Middleware>();
// app.UseOcelot(new CustomAuthorizationMiddleware(tokenValidationParameters)).Wait();

app.UseOcelot().Wait();
app.Run();