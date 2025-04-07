using ChamaOSindico.Application.Services;
using ChamaOSindico.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ITokenBlackListRepository _tokenBlackListRepository;

        public AuthController(AuthService authService, ITokenBlackListRepository tokenBlackListRepository)
        {
            _authService = authService;
            _tokenBlackListRepository = tokenBlackListRepository;
        }

        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser([FromBody] AuthDto auth)
        {
            var token = await _authService.RegisterUserAsync(auth.email, auth.password, auth.role);
            return Ok(new {token});
        }

        [HttpPost(nameof(LoginUser))]
        public async Task<IActionResult> LoginUser([FromBody] AuthDto auth)
        {
            var token = await _authService.LoginAsync(auth.email, auth.password);
            return Ok(new {token});
        }

        [HttpPost(nameof(LogoutUser))]
        public async Task<IActionResult> LogoutUser()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return BadRequest("Token not provided.");
            }
                
            var token = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var expiration = jwtToken.ValidTo;

                await _tokenBlackListRepository.AddTokenToBlackListAsync(token, expiration);
                return Ok(new { message = "Successfully logged out." });
            }
            catch
            {
                return BadRequest("Invalid token.");
            }
        }
    }

    public record AuthDto(string email, string password, string? role);
}
