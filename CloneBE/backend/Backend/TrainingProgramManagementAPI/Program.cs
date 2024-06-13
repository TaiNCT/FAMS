using Amazon;
using Amazon.S3;
using Entities.Context;
using TrainingProgramManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Confugration
var _configuration = builder.Configuration;

// Getting environment variable
var isdev = builder.Environment.IsDevelopment();

// Applying connection string
string conn;
if (isdev)
{
    Console.WriteLine("[INFO]: Running in development mode.");
    // Use SQL Server for development
    builder.Services.AddDbContext<FamsContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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


// Add Mapping profiles
var mapper = new MapperConfiguration(mc =>
{
    mc.AddProfile<MappingProfile>();
});
builder.Services.AddSingleton(mapper.CreateMapper());


// Add Application Scoped
builder.Services.AddScoped<DatabaseInitialiser>();

// Config AppSettings
builder.Services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
builder.Services.Configure<FirebaseCredentials>(_configuration.GetSection("FirebaseCredentials"));

// Add Services to Controllers
builder.Services.AddControllers();

// Add Authentication
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Authorization
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IFirebaseService, FirebaseService>();
builder.Services.AddSingleton<IAmazonS3, AmazonS3Client>();
builder.Services.AddScoped<IS3Service, S3Service>();
AWSConfigs.AWSRegion = "ap-southeast-1";


// Add CORS
builder.Services.AddCors(p => p.AddPolicy("Cors", policy =>
{
    policy.WithOrigins("*")
          .AllowAnyHeader()
          .AllowAnyMethod();
}));


builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Hook into application lifetime events and trigger only application fully started
app.Lifetime.ApplicationStarted.Register(async () =>
{
    // Database Initialiser
    await app.InitialiseDatabaseAsync();
});

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("=> Development is enabled");
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Define fluent validation middleware exception handler
app.UseFluentValidationExceptionHandler();
app.UseAuthentication();
app.UseCors("Cors");
app.UseAuthorization();
app.MapControllers();
app.Run();
