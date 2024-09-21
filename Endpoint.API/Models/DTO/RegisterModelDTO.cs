using System.ComponentModel.DataAnnotations;

namespace Endpoint.API.Models.DTO
{
    public class RegisterModelDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is mandatory.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        public string? Password { get; set; }
    }
}
