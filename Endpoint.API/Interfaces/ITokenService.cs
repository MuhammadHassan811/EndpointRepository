using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Endpoint.API.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration);
    }
}
