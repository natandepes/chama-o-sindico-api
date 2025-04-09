using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        // controller de teste de integração com o mongo
        
        private readonly VehicleRepository _vehicleRepository;

        public VehicleController(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet("GetAllVehicles")]
        
        public async Task<IActionResult> GetAllVehicles()
        {
            var listVehicles = await _vehicleRepository.GetAllVehicles();
            return Ok(listVehicles);
        }

        [HttpGet("GetVehicleById/{id}")]
        public async Task<IActionResult> GetVehicleById(string idVehicle)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(idVehicle);
            return Ok(vehicle);
        }

        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            await _vehicleRepository.CreateVehicle(vehicle);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string idVehicle, Vehicle vehicle)
        {
            await _vehicleRepository.UpdateVehicleAsync(idVehicle, vehicle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(string idVehicle)
        {
            await _vehicleRepository.DeleteVehicleAsync(idVehicle);
            return Ok();
        }
    }
}
