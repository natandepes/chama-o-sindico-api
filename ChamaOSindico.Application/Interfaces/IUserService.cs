using ChamaOSindico.Application.DTOs.Auth;
using ChamaOSindico.Application.DTOs.User;
using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByEmailAsync(string email);
        Task<UserResponseDto> GetUserByIdAsync(string id);
        Task<UserPasswordDto> GetUserHashedPassword(string id);
        Task<UserResponseDto> CreateUserAsync(User newUser);
    }
}
