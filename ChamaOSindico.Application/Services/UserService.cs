using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.DTOs.User;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Security;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;

namespace ChamaOSindico.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> CreateUserAsync(RegisterUserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                PasswordHash = PasswordHasher.Hash(userDto.Password),
                Role = userDto.Role,
                PersonId = userDto.PersonId
            };

            await _userRepository.CreateUserAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                PersonId = user.PersonId
            };
        }

        public async Task<UserResponseDto> GetUserByEmailAsync(string email)
        {
            var foundUser = await _userRepository.GetUserByEmailAsync(email);

            if (foundUser == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = foundUser.Id,
                Email = foundUser.Email,
                Role = foundUser.Role.ToString(),
                PersonId = foundUser.PersonId
            };
        }

        public async Task<UserResponseDto> GetUserByIdAsync(string id)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(id);
            if (foundUser == null)
            {
                return null;
            }
            return new UserResponseDto
            {
                Id = foundUser.Id,
                Email = foundUser.Email,
                Role = foundUser.Role.ToString(),
                PersonId = foundUser.PersonId
            };
        }

        public async Task<UserPasswordDto> GetUserHashedPassword(string id)
        {
            var foundPassword = await _userRepository.GetUserHashedPassword(id);
            
            if (foundPassword == null)
            {
                return null;
            }
            
            return new UserPasswordDto
            {
                PasswordHash = foundPassword
            };
        }
    }
}
