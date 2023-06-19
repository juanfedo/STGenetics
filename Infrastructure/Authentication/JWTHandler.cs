using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JWTHandler : IJWTHandler
    {
        private readonly string? secretKey;

        public JWTHandler(IOptions<AppSettingsModel> settings)
        {
            secretKey = settings.Value.Secretkey;
        }

        public string CreateToken(string login)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey ?? string.Empty);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login));

            var tokkenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokkenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }
    }
}
