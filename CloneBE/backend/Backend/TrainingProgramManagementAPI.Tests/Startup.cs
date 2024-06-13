using Microsoft.Extensions.DependencyInjection;
using TrainingProgramManagementAPI.Services;

// [assembly: TestFramework("TrainingProgramManagementAPI.Tests.Startup", "TrainingProgramManagementAPI.Tests")]
// [assembly: TestFramework("Xunit.DependencyInjection.TestFramework", "Xunit.DependencyInjection")]
namespace TrainingProgramManagementAPI.Tests
{
    public class Startup 
    {
        // Those code execute successfully if already installed
        // Xunit.DependencyInjection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFirebaseService, FirebaseService>();
        }
    }
}