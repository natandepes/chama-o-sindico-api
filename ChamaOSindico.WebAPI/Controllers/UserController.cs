using ChamaOSindico.Infra.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(nameof(GetUserDetails))]
        public async Task<IActionResult> GetUserDetails()
        {
            // Grab the email (or ID) from claims
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim missing");
            }
                
            // Fetch user from repository
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            } 

            // Return sanitized user data (no password hash!)
            var userDto = new
            {
                user.Id,
                user.Email,
                user.Role
            };

            return Ok(userDto);
        }
    }
}
