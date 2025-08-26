using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Authentication.DbObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsAPI.Authentication
{
    public class TokenService(IConfiguration config) : ITokenService
    {

        private readonly string? key = config["Authentication:Key"];

        public string Generate(User user)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var handler = new JwtSecurityTokenHandler();

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            if (user.Role is not null)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            }

            return ci;
        }
    }
}
