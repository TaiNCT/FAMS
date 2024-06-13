
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NLog;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using SyllabusManagementAPI.Contracts;
using SyllabusManagementAPI.Extensions;
using SyllabusManagementAPI.Repository;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
// Logger
builder.Services.ConfigureLoggerService();
// Repository Wapper
builder.Services.ConfigureRepositoryWrapper();
// Service Wrapper
builder.Services.ConfigureServiceWrapper();
builder.Services.AddControllers();
// DB Connection
builder.Services.ConfigureMSSqlContext(builder.Configuration);
// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Response Handler
builder.Services.ConfigureReponseHandler();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

//builder.Services.AddScoped<FamsContext>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

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