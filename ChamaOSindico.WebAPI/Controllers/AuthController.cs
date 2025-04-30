using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var response = await _authService.RegisterUserAsync(registerUserDto);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost(nameof(LoginUser))]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var response = await _authService.LoginAsync(loginUserDto);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost(nameof(LogoutUser))]
        public async Task<IActionResult> LogoutUser()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                var error = ApiResponse<string>.ErrorResult("Token not provided.", HttpStatusCode.BadRequest);
                return StatusCode(error.StatusCode, error);
            }

            var token = authHeader["Bearer ".Length..].Trim();

            var result = await _authService.LogoutAsync(token);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete(nameof(DeleteUser) + "/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var response = await _authService.DeleteUserAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
