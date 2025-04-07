using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.Security;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Repository;

namespace ChamaOSindico.Application.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterUserAsync(string email, string password, string role)
        {
            var existing = await _userRepository.GetUserByEmailAsync(email);

            if (existing != null)
            {
                throw new Exception("User already exists");
            }

            var user = new User
            {
                Email = email,
                PasswordHash = PasswordHasher.Hash(password),
                Role = role
            };

            await _userRepository.CreateUserAsync(user);
            return _jwtService.GenerateToken(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return _jwtService.GenerateToken(user);
        }
    }
}
