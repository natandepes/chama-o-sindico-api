using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.DTOs.User;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByEmailAsync(string email);
        Task<UserResponseDto> GetUserByIdAsync(string id);
        Task<UserPasswordDto> GetUserHashedPassword(string id);
        Task<UserResponseDto> CreateUserAsync(RegisterUserDto userDto);
    }
}
