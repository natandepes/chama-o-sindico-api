using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;

namespace ChamaOSindico.Application.Services
{
    public class WarningService : IWarningService
    {
        private readonly IWarningRepository _warningRepository;

        public WarningService(IWarningRepository warningRepository)
        {
            _warningRepository = warningRepository;
        }

        public async Task<ApiResponse<string>> CreateWarningAsync(Warning warning)
        {
            if (warning.CreatedAt == default)
            {
                warning.CreatedAt = DateTime.Now;
            }

            await _warningRepository.CreateWarningAsync(warning);

            return ApiResponse<string>.SuccessResult(null, "Aviso criado com sucesso");
        }

        public async Task<ApiResponse<string>> DeleteWarningAsync(string id)
        {
            await _warningRepository.DeleteWarningAsync(id);
            return ApiResponse<string>.SuccessResult(null, "Aviso excluído com sucesso");
        }

        public async Task<ApiResponse<List<Warning>>> GetAllWarningsAsync()
        {
            var warnings = await _warningRepository.GetAllWarningsAsync();
            return ApiResponse<List<Warning>>.SuccessResult(warnings, "Avisos recuperados com sucesso");
        }
    }
}
