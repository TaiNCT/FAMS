using Contracts;
using Entities.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using System.Text;
using UserManagementAPI.Consumers;
using UserManagementAPI.Extensions;
using UserManagementAPI.Models.DTO;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// Determine the environment
var isdev = builder.Environment.IsDevelopment();

// Applying connection string
if (isdev)
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(options =>
    {
        options.UseSqlServer(conn);
    });
}
else
{
    conn = Environment.GetEnvironmentVariable("ConnectionString");

    Console.WriteLine("[INFO]: Running in production mode.");

    Console.WriteLine(conn);
    // Use SQL Server for production
    builder.Services.AddDbContext<FamsContext>(options => options.UseSqlServer(conn));
}




// Add Mass Transit
builder.Services.AddMassTransit(x =>
{
    // Adding Consumer
    x.AddConsumersFromNamespaceContaining<IdentityCreatedConsumer>();

    // x.SetEndpointNameFormatter(
    //         new KebabCaseEndpointNameFormatter("user", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure RabbitMQ host
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host => {
            // Default rabbitMq authentication
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});

// Add controller
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
// Add Cors Policy
builder.Services.AddCors(p => p.AddPolicy("UserManagement", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add Mapping profiles
builder.Services.AddAutoMapper(typeof(MappingConfig));
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration, builder.Environment.EnvironmentName))
    .Enrich.WithProperty("Environment", builder.Environment)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddSingleton<IEmailService, EmailService>();
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("UserManagement");

app.UseAuthorization();

app.MapControllers();

app.Run();

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM-dd}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}



