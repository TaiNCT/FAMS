using Entities.Context;
using Entities.Models;

namespace TrainingProgramManagementAPI.Utils
{
    public class JwtHelper
    {
        private readonly AppSettings _appsettings;

        public JwtHelper(AppSettings appSettings)
        {
            _appsettings = appSettings;
        }

        // Generate JWT token 
        public string GenerateJWTToken(User user)
        {
            // Get secret key
            var secretKeyBytes = UTF8Encoding.UTF8.GetBytes(_appsettings.SecretKey);

            // Jwt Handler
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Token claims
            List<Claim> claims = new()
            {
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Name, user.Username),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new ("userId", user.UserId),
                new (ClaimTypes.Role, user?.Role?.RoleName!)
            };

            // Token descriptor 
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // Token claims (email, role, username, id...)
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_appsettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512)
            };

            // Generate token with descriptor
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}