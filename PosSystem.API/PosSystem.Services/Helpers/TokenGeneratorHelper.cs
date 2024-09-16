using Microsoft.IdentityModel.Tokens;
using PosSystem.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PosSystem.Services.Helpers
{
    public class TokenGeneratorHelper(IConfiguration configuration)
    {
        private static readonly TimeSpan TokenExpiration = TimeSpan.FromHours(8);
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var strKey = configuration.GetValue<string>("Jwt:Key");
            var issuer = configuration.GetValue<string>("Jwt:Issuer");
            var audience = configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(strKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.Add(TokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
