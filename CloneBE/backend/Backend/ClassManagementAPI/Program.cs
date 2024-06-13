using ClassManagementAPI.Extension;
using Entities.Models;
using ClassManagementAPI.Repositories;
using ClassManagementAPI.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using Nest;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;
using Entities.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
        "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
        "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORSPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true));
});

// Determine the environment
var environment = builder.Environment.IsDevelopment() ? "Development" : "Production";
string conn;

// Conditionally configure the database provider
if (environment == "Development")
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    conn = Environment.GetEnvironmentVariable("ConnectionString");

    Console.WriteLine("[INFO]: Running in production mode.");

    Console.WriteLine(conn);
    // Use SQL Server for production
    builder.Services.AddDbContext<FamsContext>(options => options.UseSqlServer(conn));
}


builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

builder.Services.AddScoped<FamsContext>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IFSURepository, FSURepository>();
builder.Services.AddScoped<ITrainerRepository, TrainnerRepository>();
builder.Services.AddScoped<IAttendeeTypeRepository, AttendeeTypeRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassUserRepository, ClassUserRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseAuthorization();
app.UseAuthentication();

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