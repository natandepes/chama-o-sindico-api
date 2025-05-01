using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Auth;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResultDto>> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<ApiResponse<AuthResultDto>> LoginAsync(LoginUserDto loginUserDto);
        Task<ApiResponse<string>> LogoutAsync(string token);
        Task<ApiResponse<string>> DeleteUserAsync(string userId);
    }
}
