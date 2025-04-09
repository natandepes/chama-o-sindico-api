using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
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
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet(nameof(GetAllVehicles))]
        
        public async Task<IActionResult> GetAllVehicles()
        {
            var listVehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(listVehicles);
        }

        [HttpGet(nameof(GetAllVehiclesByUserId))]
        public async Task<IActionResult> GetAllVehiclesByUserId()
        {
            var userId = User.GetUserId();
            
            if (userId == null)
            {
                return StatusCode(401, "User not authenticated.");
            }
            
            var listVehicles = await _vehicleService.GetAllVehiclesByUserIdAsync(userId);
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

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

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
            await _vehicleService.CreateVehicleAsync(vehicle);
            return Ok();
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public async Task<IActionResult> Update(string id, Vehicle vehicle)
        {
            await _vehicleService.UpdateVehicleAsync(id, vehicle);
            return Ok();
        }

        [HttpDelete(nameof(DeleteVehicle) + "/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return Ok();
        }
    }
}
