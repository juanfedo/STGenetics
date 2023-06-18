using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Authentication
{
    public class JWTHandler : IJWTHandler
    {
        private readonly string? secretKey;

        public JWTHandler(IConfiguration config)
        {
            secretKey = "Microsoft.Extensions.Configuration.ConfigurationSection";// config.GetSection("settings").GetSection("secretkey").ToString();
        }

        public string CreateToken(string login)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
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
