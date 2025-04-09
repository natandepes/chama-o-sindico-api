using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {   
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("GetAllVehicles")]
        
        public async Task<IActionResult> GetAllVehicles()
        {
            var listVehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(listVehicles);
        }

        [HttpGet("GetVehicleById/{id}")]
        public async Task<IActionResult> GetVehicleById(string idVehicle)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(idVehicle);
            return Ok(vehicle);
        }

        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            await _vehicleService.CreateVehicleAsync(vehicle);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string idVehicle, Vehicle vehicle)
        {
            await _vehicleService.UpdateVehicleAsync(idVehicle, vehicle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(string idVehicle)
        {
            await _vehicleService.DeleteVehicleAsync(idVehicle);
            return Ok();
        }
    }
}
