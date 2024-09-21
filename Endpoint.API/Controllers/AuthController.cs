using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Endpoint.API.Interfaces;
using Endpoint.API.Models;
using Endpoint.API.Models.DTO;

namespace Endpoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixedwindow")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> signInManager, IConfiguration config)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "Login user.")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username!);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _config);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [SwaggerOperation(Summary = "Register a new user.",
            Description = "Registers a new user in the system. The password must contain at least one capital letter and one special symbol.")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO modelDTO)
        {
            var userExists = await _userManager.FindByNameAsync(modelDTO.Username!);

            if (userExists != null)
                return BadRequest("User already exists!");

            ApplicationUser user = new()
            {
                Email = modelDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = modelDTO.Username
            };

            var result = await _userManager.CreateAsync(user, modelDTO.Password);

            if (!result.Succeeded)
                return BadRequest("There was an error while registering, try a stronger password \n" + result.Errors);

            return Ok(new { Username = user.UserName, Email = user.Email });
        }
    }
}
