using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet(nameof(GetAllAreas))]
        public async Task<IActionResult> GetAllAreas()
        {
            var response = await _areaService.GetAllAreasAsync();
            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAreaById) + "/{id}")]
        public async Task<IActionResult> GetAreaById(string id)
        {
            var response = await _areaService.GetAreaByIdAsync(id);
            if (response == null)
            {
                return NotFound("Area not found");
            }
            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost(nameof(SaveArea))]
        public async Task<IActionResult> SaveArea([FromBody] AreaDTO areaDto)
        {
            if (areaDto == null)
            {
                return BadRequest("Area data is null");
            }

            var response = await _areaService.SaveAreaAsync(areaDto);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpDelete(nameof(DeleteArea) + "/{id}")]
        public async Task<IActionResult> DeleteArea(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Area ID is null or empty");
            }

            var response = await _areaService.DeleteAreaAsync(id);
            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAllAreaReservations))]
        public async Task<IActionResult> GetAllAreaReservations()
        {
            var response = await _areaService.GetAllAreaReservationsAsync();

            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAllAreaReservationsByUser) + "/{id}")]
        public async Task<IActionResult> GetAllAreaReservationsByUser(string id)
        {
            var response = await _areaService.GetAllAreaReservationsByUserAsync(id);


            return StatusCode(Response.StatusCode, response);
        }

        [HttpPost(nameof(SaveAreaReservation))]
        public async Task<IActionResult> SaveAreaReservation([FromBody] AreaReservationDTO areaReservationDTO)
        {
            var response = await _areaService.SaveAreaReservationAsync(areaReservationDTO);

            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAreaReservationById) + "/{id}")]
        public async Task<IActionResult> GetAreaReservationById(string id)
        {
            var response = await _areaService.GetAreaReservationByIdAsync(id);

            if (response == null)
            {
                return NotFound("Area reservation not found");
            }

            return StatusCode(Response.StatusCode, response);
        }

        [HttpDelete(nameof(DeleteAreaReservation) + "/{id}")]
        public async Task<IActionResult> DeleteAreaReservation(string id)
        {
            var response = await _areaService.DeleteAreaReservationAsync(id);


            return StatusCode(Response.StatusCode, response);
        }
    }

}
