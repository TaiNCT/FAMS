using Contracts.UserManagement;
using System.Data.Common;

namespace IdentityAPI.Consumers;


public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCreatedConsumer(UserManager<ApplicationUser> userManager,IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    //  Summary:
    //      Consume add new Identity from IdentityService then synchronized data
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        try
        {
            Console.WriteLine("--> Consuming User Created with email: {0}", context.Message.Email);

            var applicationUser = _mapper.Map<ApplicationUser>(context.Message);
            // Map to Entity
            var result = await _userManager.CreateAsync(applicationUser, context.Message.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimsAsync(applicationUser, new[]{
                new Claim(JwtClaimTypes.Name, applicationUser.UserName!),
                new Claim(JwtClaimTypes.Email, applicationUser.Email!)});
            }

            Console.WriteLine("--> Complete consuming User Created...");
        }
        catch (Exception ex) when (ex is DbException)
        {
            // Handle error using EntityFramworkOutbox
            // Process retry message
        }
    }
}
