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

        [HttpGet(nameof(GetById) + "/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var resident = await _residentRepository.GetResidentByIdAsync(id);
            
            if (resident == null)
            {
                return NotFound("Resident not found.");
            }
            
            return Ok(resident);
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public async Task<IActionResult> Update(string id, Resident resident)
        {
            var existingResident = await _residentRepository.GetResidentByIdAsync(id);
            
            if (existingResident == null)
            {
                return NotFound("Resident not found.");
            }
            
            await _residentRepository.UpdateResidentAsync(id, resident);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resident updated successfully."));
        }

        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingResident = await _residentRepository.GetResidentByIdAsync(id);

            if (existingResident == null)
            {
                return NotFound("Resident not found.");
            }

            await _residentRepository.DeleteResidentAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resident deleted successfully."));
        }
    }
}
