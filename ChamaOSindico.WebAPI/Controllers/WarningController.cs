using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WarningController : ControllerBase
    {
        private readonly IWarningService warningService;

        public WarningController(IWarningService warningService)
        {
            this.warningService = warningService;
        }

        [HttpPost(nameof(CreateWarning))]
        public async Task<IActionResult> CreateWarning([FromBody] Warning warning)
        {
            var response = await warningService.CreateWarningAsync(warning);
            
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet(nameof(GetAllWarnings))]
        public async Task<IActionResult> GetAllWarnings()
        {
            var response = await warningService.GetAllWarningsAsync();

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete(nameof(DeleteWarning) + "/{id}")]
        public async Task<IActionResult> DeleteWarning(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Warning ID is null or empty");
            }
            var response = await warningService.DeleteWarningAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
