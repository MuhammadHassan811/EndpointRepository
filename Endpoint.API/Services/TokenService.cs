using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Endpoint.API.Interfaces;

namespace Endpoint.API.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration)
        {
            var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
                throw new InvalidOperationException("Invalid JWT generation key");

            var privateKey = Encoding.UTF8.GetBytes(key);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey)
                , SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetSection("JWT").GetValue<int>("TokenExpirationInMinutes")),
                Audience = configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        }
    }
}
