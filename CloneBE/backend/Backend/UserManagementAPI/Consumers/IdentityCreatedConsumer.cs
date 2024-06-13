using System.Data.Common;
using AutoMapper;
using Contracts.IdentityManagement;
using Entities.Context;
using Entities.Models;
using MassTransit;

namespace UserManagementAPI.Consumers
{
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

                // End process..
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
}

