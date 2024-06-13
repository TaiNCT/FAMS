using Entities.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ScoreManagementAPI.MappingConfig;
using ScoreManagementAPI.Repository;
using ScoreManagementAPI.Singleton;

var builder = WebApplication.CreateBuilder(args);

// Determine the environment
var environment = builder.Environment.IsDevelopment() ? "Development" : "Production";
string conn;
string rabbitmqHost;

// Conditionally configure the database provider
if (environment == "Development")
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Phase2Database")));

    builder.Services.AddMassTransit(x =>
    {

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

}
else
{
    conn = Environment.GetEnvironmentVariable("ConnectionString");
    rabbitmqHost = Environment.GetEnvironmentVariable("rabbitmqHost");

    Console.WriteLine("[INFO]: Running in production mode.");

    Console.WriteLine(conn);
    Console.WriteLine(rabbitmqHost);

    // Use SQL Server for production
    builder.Services.AddDbContext<FamsContext>(options => options.UseSqlServer(conn));

    // Add Mass Transit
    builder.Services.AddMassTransit(x =>
    {

        x.UsingRabbitMq((context, cfg) =>
        {
            // Configure RabbitMQ host
            cfg.Host(rabbitmqHost, "/", host =>
            {
                // Default rabbitMq authentication
                host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
            });
            cfg.ConfigureEndpoints(context);
        });
    });
}

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});


// Add a host to send email
builder.Services.AddSingleton<Store>(new Store
{
    emailhost = (environment == "Development" ? "http://localhost:5004" : "http://email-svc:5004")
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<ICertRepository, CertRepository>();
builder.Services.AddScoped<IimportExcelScoreRepository, ImportExcelScoreRepository>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("=> Development is enabled");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins); // Allow CORS
app.Run();
