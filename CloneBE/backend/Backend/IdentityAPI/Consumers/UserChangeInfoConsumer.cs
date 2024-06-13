using System.Data.Common;
using System.Text.Json;
using Contracts.UserManagement;
using Entities.Context;
using Entities.Models;

namespace IdentityAPI.Consumers;

public class UserChangeInfoConsumer : IConsumer<UserChangeInfo>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    // Default constructor
    // Argument contructor
    public UserChangeInfoConsumer(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UserChangeInfo> context)
    {
        try
        {
            if (context.Message is null)
            {
                Console.WriteLine("--> Not found any message to consume, please check again...");
            }

            Console.WriteLine("--> Consuming update user with email: {0}", context.Message?.Email);

            // Process update user ...
            // Map to entity 
            var updateIdentityEntity = _mapper.Map<ApplicationUser>(context.Message);

            // Get user by username
            var identityEntity = await _userManager.FindByNameAsync(context.Message!.Username);
            if (identityEntity is null) Console.WriteLine("Not found any identity model match username {0}", context.Message!.Username);

            // Map properties from updateIdentityEntity to identityEntity
            Console.WriteLine("From User: ");
            Console.WriteLine(JsonSerializer.Serialize(context.Message));

            Console.WriteLine("From Identity: ");
            Console.WriteLine(JsonSerializer.Serialize(updateIdentityEntity));

            Console.WriteLine("Updated: ");
            Console.WriteLine(JsonSerializer.Serialize(identityEntity));
            identityEntity.FullName = updateIdentityEntity.FullName;
            identityEntity.Phone = updateIdentityEntity.Phone;
            identityEntity.Address = updateIdentityEntity.Address;
            identityEntity.Status = updateIdentityEntity.Status;
            identityEntity.Dob = updateIdentityEntity.Dob;
            identityEntity.ModifiedDate = updateIdentityEntity.ModifiedDate;
            identityEntity.Email = updateIdentityEntity.Email;
            identityEntity.Gender = updateIdentityEntity.Gender;
            //identityEntity = _mapper.Map(updateIdentityEntity, identityEntity);
            var result = await _userManager.UpdateAsync(identityEntity!);
            if (result.Succeeded) Console.WriteLine("--> Consume update info user successfully");
        }
        catch (Exception ex) when (ex is DbException)
        {
            // Handle error using EntityFramworkOutbox
            // Process retry message
        }
    }
}