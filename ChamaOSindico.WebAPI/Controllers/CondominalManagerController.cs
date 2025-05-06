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
    }
}
