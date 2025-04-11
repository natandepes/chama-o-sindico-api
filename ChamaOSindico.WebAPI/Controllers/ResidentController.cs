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
        public IActionResult GetAll()
        {
            var listResidents = _residentRepository.GetAllResidentsAsync().Result;
            return Ok(listResidents);
        }

        [HttpGet(nameof(GetById) + "/{id}")]
        public IActionResult GetById(string id)
        {
            var resident = _residentRepository.GetResidentByIdAsync(id).Result;
            
            if (resident == null)
            {
                return NotFound("Resident not found.");
            }
            
            return Ok(resident);
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public IActionResult Update(string id, Resident resident)
        {
            var existingResident = _residentRepository.GetResidentByIdAsync(id).Result;
            
            if (existingResident == null)
            {
                return NotFound("Resident not found.");
            }
            
            _residentRepository.UpdateResident(id, resident);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resident updated successfully."));
        }

        [HttpDelete(nameof(Delete) + "/{id}")]
        public IActionResult Delete(string id)
        {
            var existingResident = _residentRepository.GetResidentByIdAsync(id).Result;

            if (existingResident == null)
            {
                return NotFound("Resident not found.");
            }

            _residentRepository.DeleteResident(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resident deleted successfully."));
        }
    }
}
