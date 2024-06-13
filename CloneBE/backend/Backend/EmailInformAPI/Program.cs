using EmailInformAPI.Extensions;
using EmailInformAPI.Middleware;
using EmailInformAPI.Repository;
using EmailInformAPI.Scheduler;
using Entities;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors",
        builder => builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IScheduler, Scheduler>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


// Conditionally configure the database provider
var environment = builder.Environment.IsDevelopment() ? "Development" : "Production";
string conn;
if (environment == "Development")
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<Middleware>();
app.UseCors("MyCors");

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