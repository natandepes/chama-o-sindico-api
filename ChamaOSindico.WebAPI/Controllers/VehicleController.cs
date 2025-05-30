using ChamaOSindico.Application.DTOs.Vehicles;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            var response = await _vehicleService.GetAllVehicles();
            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAllVehiclesByUserId))]
        public async Task<IActionResult> GetAllVehiclesByUserId()
        {
            var userId = User.GetUserId();
            var response = await _vehicleService.GetAllVehiclesByUserId(userId!);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetVehicleById) + "/{id}")]
        public async Task<IActionResult> GetVehicleById(string id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return StatusCode(401, "User not authenticated.");
            }

            var response = await _vehicleService.GetVehicleById(id);

            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost(nameof(SaveVehicle))]
        public async Task<IActionResult> SaveVehicle([FromBody] VehicleDto vehicle)
        {
            if (vehicle.CreatedByUserId.IsNullOrEmpty())
            {
                vehicle.CreatedByUserId = User.GetUserId();
            }
          
            var response = await _vehicleService.SaveVehicle(vehicle);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpDelete(nameof(DeleteVehicle) + "/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            var response = await _vehicleService.DeleteVehicle(id);
            return StatusCode(Response.StatusCode, response);
        }
    }
}
