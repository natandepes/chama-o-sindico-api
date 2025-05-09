using ChamaOSindico.Application.Commom;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CondominalManagerController : ControllerBase
    {
        private readonly ICondominalManagerRepository _condominalManagerRepository;

        public CondominalManagerController(ICondominalManagerRepository condominalManagerRepository)
        {
            _condominalManagerRepository = condominalManagerRepository;
        }

        [HttpGet(nameof(GetCurrentCondominalManager))]
        public async Task<IActionResult> GetCurrentCondominalManager()
        {
            var condominalManager = await _condominalManagerRepository.GetCurrentCondominalManager();
            return Ok(condominalManager);
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(CondominalManager condominalManager)
        {
            var existingCondominalManager = await _condominalManagerRepository.GetCondominalManagerByUserIdAsync(condominalManager.UserId);

            if (existingCondominalManager == null)
            {
                return NotFound("Condominal manager not found.");
            }

            await _condominalManagerRepository.UpdateResidentAsync(existingCondominalManager.Id, condominalManager);
            return Ok(ApiResponse<string>.SuccessResult(null, "Informações pessoais atualizadas com sucesso."));
        }
    }
}
