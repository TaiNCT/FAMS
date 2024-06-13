# FAMS - Contracts

## Get started

This project is an Class Library project serving as intermediary for synchronizing data between two or more services.

## Overview - RabbitMQ

- [ ] [RabbitMQ](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet)

Message Broker: Accept and Forward messages + Accept: move message into an exchange + Forward: forward message to a queue that services can subscribe

Producer/Consumer model (Publish/Subscribe)

Message are stored on queues (message buffer), so RabbitMQ also has persistence associated with it too
-> It's able to create new one if fail, then, recieve message stored inside message buffer

Exchange can be used for "routing" functionality
-> When message published, we send it to exchange and exchange has queue bound to it

## Overview - MassTransit

- [ ] [MassTransit](https://masstransit.io/documentation/configuration)

MassTransit fully supports RabbitMQ, including many of the advanced features and capabilities. This is going to abstract the need for us to make direct connection and use RabbitMQ API directly

## MassTransit Installation

Please install MassTransit package to your service before we reference to **_"Contracts"_** project.

For .NET CLI:

```
dotnet add package MassTransit.RabbitMQ --version 8.2.0
```

For Package Manager:

```
NuGet\Install-Package MassTransit.RabbitMQ -Version 8.2.0
```

Add project reference .NET CLI:

```
cd [your_service]
dotnet add reference ../Contracts
```

## How to use this project

1. Create folder with name is your service name

```
Contracts
    └ IdentityManagement
    └ TrainingProgramManagement
    └ SyllabusManagement
    └ UserManagement
    └ ...
```

2. Create unique class that serves for synchronize data when [CREATE, UPDATE, REMOVE,...] to other service

```
Contracts
    └ IdentityManagement
        └ IdentityCreated.cs
        └ IdentityDeleted.cs
        └ IdentityUpdated.cs
        └ ...
```

FOR CONSUMER (UserManagement)

3.1. Create class as RabbitMQ Consumer related to those classes on Contracts in your service project. For Example:

```
UserManagementAPI
    └ Consumers
        └ IdentityCreatedConsumer.cs
        └ IdentityDeletedConsumer.cs
        └ IdentityUpdatedConsumer.cs
        └ ...
```

3.2. Implement consumer handling (Subscribe message from Message Queue)

```
UserManagementAPI
    └ Consumers
        └ IdentityCreatedConsumer.cs


using AutoMapper;
using Contracts.IdentityManagement;
using Entities.Context;
using Entities.Models;
using MassTransit;

namespace UserManagementAPI.Consumers;

public class IdentityCreatedConsumer : IConsumer<IdentityCreated>
{
    private readonly IMapper _mapper;
    private readonly FamsContext _dbContext;

    public IdentityCreatedConsumer(IMapper mapper,
        FamsContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    //  Summary:
    //      Consume add new Identity from IdentityService then synchronized data
    public async Task Consume(ConsumeContext<IdentityCreated> context)
    {
        try
        {
            Console.WriteLine("--> Consuming Identity Created with email: {0}", context.Message.Email);

            // Map to Entity
            var user = _mapper.Map<User>(context.Message);
            // Add new Entity
            await _dbContext.Users.AddAsync(user);
            // Savechange DB
            await _dbContext.SaveChangesAsync();

            // End process...
            await Task.CompletedTask;

            Console.WriteLine("--> Complete consuming Identity Created...");
        }
        catch (Exception ex) when (ex is DbException)
        {
            // Handle error using EntityFramworkOutbox
            // Process retry message
        }
    }
}
```

3.3. Config to Program.cs

```
// Add Mass Transit
builder.Services.AddMassTransit(x =>
{
    // Add all consumers existing from class namespace
    // This will add all consumers in namespace, so that I required to wrap all your Contracts classes in same namespace
    x.AddConsumersFromNamespaceContaining<IdentityCreatedConsumer>();

    // Add RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure RabbitMQ host
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host => {
            // Default rabbitMq authentication
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});
```

FOR PUBLISHER (Identity)

4.1. Implement publish message (Here - I want to publish message from Identity to UserManagement)

```
IdentityAPI
    └ Controllers
        └ AuthenticationController.cs

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IPublishEndpoint publishEndpoint,
        IMapper mapper
    )
    {
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    [HttpPost(APIRoutes.Identity.Register, Name = nameof(RegisterAsync))]
    public async Task<IActionResult> RegisterAsync([FromBody] IdentityRegisterRequest reqObj)
    {
        // Convert request obj to ApplicationUser
        var applicationUser = _mapper.Map<ApplicationUser>(reqObj);

        // Process check validation here... (if any)

        // Publish message to message broker for UserManagementAPI add new user data
        // Map model to intermidate model from Contracts
        var identityCreated = _mapper.Map<IdentityCreated>(applicationUser);

        // Publish to Message Broker (RabbitMQ Exchange)
        await _publishEndpoint.Publish(identityCreated);

        // Process response here...
    }

}
```

4.2. Config to Program.cs

```
// Add Mass Transit
builder.Services.AddMassTransit(x =>
{
    // Add RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure RabbitMQ host
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host => {
            // Default rabbitMq authentication
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});
```

## NOTE:

Please ensure that you declare RabbitMQ configuration within your appsettings.json file as follows:

"RabbitMQ": {
"Host": "localhost"
}

In case of docker environment, appsettings.Docker.json (That's for me, you can declare configuration file for Docker by your own)

"RabbitMQ": {
"Host": "rabbitmq"
}