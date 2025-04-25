using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChamaOSindico.WebAPI.Controllers
{
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
            var areas = await _areaService.GetAllAreasAsync();
            return Ok(areas);
        }

        [HttpGet(nameof(GetAreaById))]
        public async Task<IActionResult> GetAreaById(string id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            if (area == null)
            {
                return NotFound("Area not found");
            }
            return Ok(area);
        }

        [HttpPost(nameof(SaveArea))]
        public async Task<IActionResult> SaveArea([FromBody] AreaDTO areaDto)
        {
            if (areaDto == null)
            {
                return BadRequest("Area data is null");
            }

            await _areaService.SaveAreaAsync(areaDto);
            return Ok("Area saved successfully");
        }

        [HttpDelete(nameof(DeleteArea))]
        public async Task<IActionResult> DeleteArea(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Area ID is null or empty");
            }

            await _areaService.DeleteAreaAsync(id);
            return Ok("Area deleted successfully");
        }

        [HttpGet(nameof(GetAllAreaReservations))]
        public async Task<IActionResult> GetAllAreaReservations()
        {
            var areaReservations = await _areaService.GetAllAreaReservationsAsync();

            return Ok(areaReservations);
        }

        [HttpGet(nameof(GetAllAreaReservationsByUser))]
        public async Task<IActionResult> GetAllAreaReservationsByUser(string id)
        {
            var areaReservations = await _areaService.GetAllAreaReservationsByUserAsync(id);


            return Ok(areaReservations);
        }

        [HttpPost(nameof(SaveAreaReservation))]
        public async Task<IActionResult> SaveAreaReservation([FromBody] AreaReservationDTO areaReservationDTO)
        {
            await _areaService.SaveAreaReservationAsync(areaReservationDTO);

            return Ok();
        }

        [HttpGet(nameof(GetAreaReservationById))]
        public async Task<IActionResult> GetAreaReservationById(string id)
        {
            var areaReservation = await _areaService.GetAreaReservationByIdAsync(id);

            if (areaReservation == null)
            {
                return NotFound("Area reservation not found");
            }

            return Ok(areaReservation);
        }

        [HttpDelete(nameof(DeleteAreaReservation))]
        public async Task<IActionResult> DeleteAreaReservation(string id)
        {
            await _areaService.DeleteAreaReservationAsync(id);


            return Ok();
        }
    }

}
