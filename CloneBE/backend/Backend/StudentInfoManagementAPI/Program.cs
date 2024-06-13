using AutoMapper;
using Contracts.StudentManagement;
using Entities.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nest;
using OJTStudentManagement;
using StudentInfoManagementAPI.Consumers;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Hubs;
using StudentInfoManagementAPI.Service;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IUploadFileDL, UploadFileDL>();
builder.Services.AddScoped<IStudentService_LamNS, StudentService_LamNS>();

bool isDevelop = builder.Environment.IsDevelopment();

if (isDevelop)
    Console.WriteLine("/* Development mode, using appsettings.Development.json");
else
    Console.WriteLine("/* Production mode, using appsettings.Docker.json");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//Config Elastic
// Inside your ConfigureServices method in Startup.cs or Program.cs
builder.Services.AddSingleton<IElasticClient>(provider =>
{
    var settings = new ConnectionSettings(new Uri(builder.Configuration["ElasticConfiguration:Uri"])).DefaultIndex("students");
    return new ElasticClient(settings);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

//Config database
builder.Services.AddDbContext<FamsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

// Add Mass Transit
builder.Services.AddMassTransit(x =>
{
    // Adding Consumer
    x.AddConsumersFromNamespaceContaining<GeneralStudentUpdatedCosumer>();
    x.AddConsumersFromNamespaceContaining<OtherStudentUpdatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure RabbitMQ host
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            // Default rabbitMq authentication
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IStudentService_LamNS, StudentService_LamNS>();
builder.Services.AddScoped<IStudentService_QuyNDC, StudentService_QuyNDC>();
builder.Services.AddScoped<IStudentDetailinClass, StudentDetailinClass>();
builder.Services.AddScoped<IEditStatusStudentInBatch, EditStatusStudentInBatch>();
builder.Services.AddScoped<IAddStudentClassService, AddStudentService>();

IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddScoped<IMapper>(_ => MapperConfig.RegisterMaps().CreateMapper());
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseWebSockets();
app.MapHub<Signal>("/signal");
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
