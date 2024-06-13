using System.Data.Common;
using Contracts.UserManagement;

namespace IdentityAPI.Consumers;

public class UserChangeStatusConsumer : IConsumer<UserChangeStatus>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserChangeStatusConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UserChangeStatus> context)
    {
        try
        {
            if (context.Message is null)
            {
                Console.WriteLine("--> Not found any message to consume, please check again...");
            }

            Console.WriteLine("--> Consuming User change role with username: {0}", context.Message!.Username);

            // Get user by username
            var identityEntity = await _userManager.FindByNameAsync(context.Message!.Username);
            if (identityEntity is null) Console.WriteLine("Not found any identity model match username {0}", context.Message!.Username);

            // Update role
            identityEntity!.Status = context.Message.Status;

            // Update to DB
            await _userManager.UpdateAsync(identityEntity!);

            Console.WriteLine("--> Complete consuming User Change role...");
        }
        catch (Exception ex) when (ex is DbException)
        {
            // Handle error using EntityFramworkOutbox
            // Process retry message
        }
    }
}