using Duende.IdentityServer.Services;
using IdentityAPI.Consumers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Serilog;
using Entities.Context;

namespace IdentityAPI;
internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add Razer Page: (Duende default)
        builder.Services.AddRazorPages();
        // Add DBContext 
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            // Add EF implementation of identity information stores
            .AddEntityFrameworkStores<ApplicationDbContext>()
            // Default token providers used to generate token for reset password, change email...
            .AddDefaultTokenProviders();

        // Add Identity Server 
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // Define IssuerUri for iss payload in token is from server or not 
                if (builder.Environment.IsEnvironment("Docker"))
                {
                    options.IssuerUri = "http://identity-svc";
                }
                else if (builder.Environment.IsEnvironment("Development"))
                {
                    options.IssuerUri = "http://localhost:5000";
                }

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                // options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            // Add custom profile 
            .AddProfileService<CustomProfileService>();

        // Getting environment variable
        var isdev = builder.Environment.IsDevelopment();

        // Add DbContext
        builder.Services.AddDbContext<FamsContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Add custom refresh token
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Environment.IsDevelopment()
                ? "http://localhost:5000"
                : "http://identity-svc",
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            RequireExpirationTime = false, // Prevent SecurityTokenExpiredException
            // Set the SignatureValidator to a delegate that always returns true 
            SignatureValidator = (token, parameters) =>
            {
                var jwt = new JsonWebToken(token);
                return jwt;
            },
            RequireSignedTokens = false
        };
        builder.Services.AddSingleton(tokenValidationParameters);
        // builder.Services.AddTransient<IRefreshTokenService, CustomRefreshTokenService2>();
        builder.Services.AddTransient<IRefreshTokenService, CustomRefreshTokenService>();
        // Add HttpContextAccessor
        builder.Services.AddHttpContextAccessor();

        // Add Automapper 
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProfilesExtension());
        });
        builder.Services.AddSingleton(mapperConfig.CreateMapper());

        // Add Message Broker (MassTransit) - RabbitMQ 
        builder.Services.AddMassTransit(x =>
        {
            // x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("identity", false));

            // Add EntityFrameWorkOutbox
            // If message is down for DbConnection, it will send to out box 
            // Resend until consumers recover again 
            // x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
            // {
            //     // After 10s, query to Identity DB to check outbox 
            //     o.QueryDelay = TimeSpan.FromSeconds(10);

            //     // Configure DB 
            //     o.UseSqlServer();
            //     o.UseBusOutbox();
            // });

            // This will add all consumers in namespace, so that I required to wrap all your Contracts classes in same namespace
            x.AddConsumersFromNamespaceContaining<UserCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
                {
                    host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
                    host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        // builder.Services.ConfigureApplicationCookie(options => {
        //     options.Cookie.SameSite = SameSiteMode.Lax;
        // });


        /// Custom authentication to login with Google, just care it later  
        // builder.Services.AddAuthentication()
        //     .AddGoogle(options =>
        //     {
        //         options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        //         // register your IdentityServer with Google at https://console.developers.google.com
        //         // enable the Google+ API
        //         // set the redirect URI to https://localhost:5001/signin-google
        //         options.ClientId = "copy client ID from Google here";
        //         options.ClientSecret = "copy client secret from Google here";
        //     });

        // Add Authentication
        builder.Services.AddAuthentication();

        // Add Controller
        builder.Services.AddControllers();

        // Add CORS 
        builder.Services.AddCors(p => p.AddPolicy("Cors", policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        // Using static files because of returning HTML, JS, CSS this server 
        // to the client (optional) - we use API to get token only
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseCors("Cors");
        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}