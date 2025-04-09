using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var token = await _authService.RegisterUserAsync(registerUserDto);
            return Ok(new {token});
        }

        [HttpPost(nameof(LoginUser))]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var token = await _authService.LoginAsync(loginUserDto);
            return Ok(new {token});
        }

        [HttpPost(nameof(LogoutUser))]
        public async Task<IActionResult> LogoutUser()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return BadRequest("Token not provided.");

            var token = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                await _authService.LogoutAsync(token);
                return Ok("Successfully logged out.");
            }
            catch
            {
                return BadRequest("Invalid token.");
            }
        }
    }
}
