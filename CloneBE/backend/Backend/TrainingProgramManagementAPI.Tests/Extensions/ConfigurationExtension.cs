using Amazon.Runtime.Internal;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Moq;
using TrainingProgramManagementAPI.Mappings;
using TrainingProgramManagementAPI.Services;
using TrainingProgramManagementAPI.Utils;

namespace TrainingProgramManagementAPITests.Extensions;

public static class ConfigurationExtension
{
    public static IServiceCollection Services { get; private set; } = null!;
    public static IServiceProvider ServiceProvider { get; private set; } = null!;
    public static FirebaseCredentials Credentials = new FirebaseCredentials
    {
        ApiKey = "AIzaSyCgrkdj4WGSKrG6bMeR2RwpiotVVg2LaUg",
        BucketName = "able-rarity-401314.appspot.com",
        AuthEmail = "phuoclx.test@gmail.com",
        AuthPassword = "Test@123",
        ProjectId = "able-rarity-401314"
    };

    public static AppSettings AppSettings = new AppSettings
    {
        SecretKey = "3FA4932AD84BA4B29D2F1E1FF28BE",
        PageSize = 5,
        TokenLifeTime = new TimeSpan(24, 0, 0),
        DateTimeFormat = "yyyy-MM-dd"
    };


    public static void ConfigureServices()
    {
        Services = new ServiceCollection();

        // Build IConfiguration from Firebase Credentails
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"FirebaseCredentials:ApiKey", Credentials.ApiKey},
                {"FirebaseCredentials:BucketName", Credentials.BucketName},
                {"FirebaseCredentials:AuthEmail", Credentials.AuthEmail},
                {"FirebaseCredentials:AuthPassword", Credentials.AuthPassword},
                {"FirebaseCredentials:ProjectId", Credentials.ProjectId}
            }).Build();

        Services.Configure<FirebaseCredentials>(configuration.GetSection("FirebaseCredentials"));

        // Configure services
        Services.AddScoped<IFirebaseService, FirebaseService>();

        ServiceProvider = Services.BuildServiceProvider();
    }

    public static IMapper ConfigureAutoMapper()
    {
        // Configure IMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        return mapperConfig.CreateMapper();
    }

    public static void ConfigureAppSettings(
        this Mock<IOptionsMonitor<AppSettings>> mockAppSettings)
    {
        mockAppSettings.SetupGet(o => o.CurrentValue).Returns(AppSettings);
    }

    public static FamsContext ConfigureDbContext()
    {
        // Default context
        // Set up an in-memory database for testing 
        var options = new DbContextOptionsBuilder<FamsContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            // don't raise the error warning us that the in memory db doesn't support transactions
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        // Initiate database context
        return new FamsContext(options);
    }

    public static void ConfigureFirebaseCredential(
        this Mock<IOptionsMonitor<FirebaseCredentials>> mockFirebaseCredentail)
    {


        mockFirebaseCredentail.Setup(fb => fb.CurrentValue).Returns(Credentials);
    }

    public static IFirebaseService ConfigureFirebaseService()
    {
        return ServiceProvider.GetRequiredService<IFirebaseService>();
    }
}