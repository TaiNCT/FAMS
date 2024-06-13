using AutoMapper;
using FakeItEasy;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Entities.Models;
using UserManagementAPI.Models.DTO;
using UserManagementAPITests.UserData;
using Entities.Context;
using MassTransit;
using UserManagementAPI.Consumers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

namespace UserManagementAPITests
{
    public class ConfigurationExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        public static IServiceCollection Services { get; private set; } = null!;

        public static FamsContext GetServiceAppContext()
        {
            var options = new DbContextOptionsBuilder<FamsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return new FamsContext(options);
        }
        public static IMapper ConfigureAutoMapper()
        {
            // Configure IMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });
            return mapperConfig.CreateMapper();
        }

        public static void ConfigureServices()
        {
            // Build service collection
            Services = new ServiceCollection();

            // Add Mass Transit
            Services.AddMassTransit(x =>
            {
                // Adding Consumer
                x.AddConsumersFromNamespaceContaining<IdentityCreatedConsumer>();

                // x.SetEndpointNameFormatter(
                //         new KebabCaseEndpointNameFormatter("user", false));

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Configure RabbitMQ host
                    cfg.Host("localhost", "/", host =>
                    {
                        // Default rabbitMq authentication
                        host.Username("guest");
                        host.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            // Build the service provider
            ServiceProvider = Services.BuildServiceProvider();
        }

        public static IPublishEndpoint ConfigurePublishEndpoint()
        {
            return ServiceProvider.GetRequiredService<IPublishEndpoint>();
        }
    }
}
