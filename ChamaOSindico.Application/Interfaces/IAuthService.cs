using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Auth;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<ApiResponse<string>> LoginAsync(LoginUserDto loginUserDto);
        Task<ApiResponse<string>> LogoutAsync(string token);
    }
}
