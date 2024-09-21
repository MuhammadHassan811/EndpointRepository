using Microsoft.AspNetCore.Identity;

namespace Endpoint.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }  
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
