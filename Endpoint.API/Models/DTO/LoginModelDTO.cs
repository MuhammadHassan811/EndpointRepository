using System.ComponentModel.DataAnnotations;

namespace Endpoint.API.Models.DTO
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        public string? Password { get; set; }
    }
}
