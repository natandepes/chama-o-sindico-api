using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Security;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace ChamaOSindico.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly ITokenBlackListRepository _tokenBlacklistRepository;
        private readonly IUserService _userService;

        public AuthService(IUserRepository userRepository, JwtService jwtService, ITokenBlackListRepository tokenBlacklistRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _tokenBlacklistRepository = tokenBlacklistRepository;
            _userService = userService;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var existing = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);

            if (existing != null)
            {
                throw new Exception("User already exists");
            }

            var createdUser = await _userService.CreateUserAsync(registerUserDto);
            
            var authDto = new AuthUserDto
            {
                Id = createdUser.Id,
                Email = registerUserDto.Email,
                Role = createdUser.Role
            };
            
            return _jwtService.GenerateToken(authDto);
        }

        public async Task<string> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginUserDto.Email);
            var userHashedPassword = await _userRepository.GetUserHashedPassword(user.Id);

            if (user == null || !PasswordHasher.Verify(loginUserDto.Password, userHashedPassword))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            userHashedPassword = null; // Clear password hash for security reasons

            var authDto = new AuthUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return _jwtService.GenerateToken(authDto);
        }

        public async Task LogoutAsync(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expiration = jwtToken.ValidTo;

            await _tokenBlacklistRepository.AddTokenToBlackListAsync(token, expiration);
        }
    }
}
