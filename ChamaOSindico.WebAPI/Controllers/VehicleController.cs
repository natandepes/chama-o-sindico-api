using ChamaOSindico.Application.Commom;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {   
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet(nameof(GetAllVehicles))]
        
        public async Task<IActionResult> GetAllVehicles()
        {
            var listVehicles = await _vehicleRepository.GetAllVehiclesAsync();
            return Ok(listVehicles);
        }

        [HttpGet(nameof(GetAllVehiclesByUserId))]
        public async Task<IActionResult> GetAllVehiclesByUserId()
        {
            var userId = User.GetUserId();
            var listVehicles = await _vehicleRepository.GetAllVehiclesByUserIdAsync(userId);
            return Ok(listVehicles);
        }

        [HttpGet(nameof(GetVehicleById) + "/{id}")]
        public async Task<IActionResult> GetVehicleById(string id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return StatusCode(401, "User not authenticated.");
            }

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound("Vehicle not found.");
            }

            if (vehicle.CreatedByUserId != userId)
            {
                return StatusCode(403, "Access denied. You do not own this vehicle");
            }

            return Ok(vehicle);
        }

        [HttpPost(nameof(CreateVehicle))]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            await _vehicleRepository.CreateVehicleAsync(vehicle);
            return Ok(ApiResponse<string>.SuccessResult(null, "Vehicle created successfully."));
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public async Task<IActionResult> Update(string id, Vehicle vehicle)
        {
            await _vehicleRepository.UpdateVehicleAsync(id, vehicle);
            return Ok(ApiResponse<string>.SuccessResult(null, "Vehicle updated successfully."));
        }

        [HttpDelete(nameof(DeleteVehicle) + "/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            await _vehicleRepository.DeleteVehicleAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Vehicle deleted successfully."));
        }
    }
}
