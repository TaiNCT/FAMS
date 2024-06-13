using System.Data.Common;
using Contracts.UserManagement;

namespace IdentityAPI.Consumers;

public class UserChangePasswordConsumer : IConsumer<UserChangePassword>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserChangePasswordConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task Consume(ConsumeContext<UserChangePassword> context)
    {
        try
        {
            if (context.Message is null)
            {
                Console.WriteLine("--> Not found any message to consume, please check again...");
            }

            Console.WriteLine("--> Consuming User change password with username: {0}", context.Message!.Username);

            // Get user by username
            var identityEntity = await _userManager.FindByNameAsync(context.Message!.Username);
            if (identityEntity is null) Console.WriteLine("Not found any identity model match username {0}", context.Message!.Username);

            // Change password
            await _userManager.ChangePasswordAsync(identityEntity!, // Application user 
                context.Message.CurrentPassword, // Current password
                context.Message!.NewPassword); // New password

            // Update to DB
            await _userManager.UpdateAsync(identityEntity!);

            Console.WriteLine("--> Complete consuming User Change password...");
        }
        catch (Exception ex) when (ex is DbException)
        {
            // Handle error using EntityFramworkOutbox
            // Process retry message
        }
    }
}