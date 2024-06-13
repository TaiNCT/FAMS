using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Entities.Context;
using IdentityAPI.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Services;

public class CustomProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly FamsContext _context;

    public CustomProfileService(UserManager<ApplicationUser> userManager,
        FamsContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        // Get user by claim principles
        var user = await _userManager.GetUserAsync(context.Subject);

        if (user is not null)
        {
            var existingClaims = await _userManager.GetClaimsAsync(user);

            // Get user role 
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId.Equals(user.RoleId));

            var claims = new List<Claim>()
            {
                // Add username claim
                new Claim(IdentityData.UsernameClaimName, user.UserName!),

                // Custom roles and other claims later
                new Claim(IdentityData.FullnameClaimName, user.FullName!),

            };

            if (userRole is not null) // Check role exist 
            {
                // Add role claim
                claims.Add(new Claim(IdentityData.RoleClaimName, userRole?.RoleName!));
                // claims.Add(new Claim(ClaimTypes.Role, userRole?.RoleName!));
            }

            // Adding claims to context 
            context.IssuedClaims.AddRange(claims);
            // context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));

        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}