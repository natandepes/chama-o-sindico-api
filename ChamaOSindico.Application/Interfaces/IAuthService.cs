using ChamaOSindico.Application.DTOs.Auth;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<string> LoginAsync(LoginUserDto loginUserDto);
        Task LogoutAsync(string token);
    }
}
