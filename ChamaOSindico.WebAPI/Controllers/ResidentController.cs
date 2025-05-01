using ChamaOSindico.Application.Commom;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentRepository _residentRepository;

        public ResidentController(IResidentRepository residentRepository)
        {
            _residentRepository = residentRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var listResidents = await _residentRepository.GetAllResidentsAsync();
            return Ok(listResidents);
        }

        [HttpGet(nameof(GetResidentDetails))]
        public async Task<IActionResult> GetResidentDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var resident = await _residentRepository.GetResidentByUserIdAsync(userId);
            
            if (resident == null)
            {
                return NotFound("Resident not found.");
            }
            
            return Ok(resident);
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(Resident resident)
        {
            var existingResident = await _residentRepository.GetResidentByIdAsync(resident.Id);
            
            if (existingResident == null)
            {
                return NotFound("Resident not found.");
            }
            
            await _residentRepository.UpdateResidentAsync(existingResident.Id, resident);
            return Ok(ApiResponse<string>.SuccessResult(null, "Informações pessoais atualizadas com sucesso."));
        }
    }
}
