using ChamaOSindico.Application.Commom;
using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IWarningService
    {
        Task<ApiResponse<string>> CreateWarningAsync(Warning warning);
        Task<ApiResponse<List<Warning>>> GetAllWarningsAsync();
        Task<ApiResponse<string>> DeleteWarningAsync(string id);
    }
}
