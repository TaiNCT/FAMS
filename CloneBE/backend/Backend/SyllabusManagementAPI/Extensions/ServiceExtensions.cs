using Microsoft.EntityFrameworkCore;
using SyllabusManagementAPI.Contracts;
using Entities.Context;
using SyllabusManagementAPI.LoggerService;
using SyllabusManagementAPI.Middleware;
using SyllabusManagementAPI.Repository;
using SyllabusManagementAPI.Service;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Extensions
{
    /// <summary>
    /// Contains extension methods for configuring services in the application.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures Cross-Origin Resource Sharing (CORS) policy for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Configures IIS integration for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        /// <summary>
        /// Configures the logger service for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Configures the MSSQL context for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <param name="config">The <see cref="IConfiguration"/> containing the connection string.</param>
        public static void ConfigureMSSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<FamsContext>(o => o.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Configures the FamsContext for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        //public static void ConfigureFamsContext(this IServiceCollection services)
        //{
        //    services.AddScoped<FamsContext>();
        //}

        /// <summary>
        /// Configures the repository wrapper for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ISyllabusRepository, SyllabusRepository>();
        }

        public static void ConfigureServiceWrapper(this IServiceCollection services)
        {
            services.AddScoped<IServiceWrapper, ServiceWrapper>();
        }

        /// <summary>
        /// Configures the response handler for the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        public static void ConfigureReponseHandler(this IServiceCollection services)
        {
            services.AddSingleton<ResponseHandler>();
        }
    }
}
