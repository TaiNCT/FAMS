using Microsoft.Extensions.DependencyInjection;
using TrainingProgramManagementAPI.Services;
using Xunit.Abstractions;

namespace TrainingProgramManagementAPITests
{
    public class TestServiceCollectionFixture : IDisposable
    {
        public IServiceCollection Services { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
        public ITestOutputHelper Output { get; private set; }

        public TestServiceCollectionFixture(ITestOutputHelper output)
        {
            Output = output;

            Services = new ServiceCollection();

            // Configure services
            Services.AddScoped<IFirebaseService, FirebaseService>();

            ServiceProvider = Services.BuildServiceProvider();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}