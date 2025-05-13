using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.AreaReservation;
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

        [HttpPost(nameof(CreateArea))]
        public async Task<IActionResult> CreateArea([FromBody] AreaDTO areaDto)
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

        [HttpGet(nameof(GetAllAreaReservationsByUser))]
        public async Task<IActionResult> GetAllAreaReservationsByUser()
        {
            var userId = User.GetUserId();
            
            var response = await _areaService.GetAllAreaReservationsByUserAsync(userId);


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

        [HttpPost(nameof(AddAnswerToAreaReservation))]
        public async Task<IActionResult> AddAnswerToAreaReservation([FromBody] AreaReservationAnswer answer)
        {
            await _areaService.AddAnswerToAreaReservationAsync(answer);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resposta adicionada com sucesso"));
        }

        [HttpPost(nameof(ChangeAreaReservationStatus))]
        public async Task<IActionResult> ChangeAreaReservationStatus([FromBody] ChangeAreaReservationStatusDto dto)
        {
            var result = await _areaService.ChangeAreaReservationStatusAsync(dto.AreaReservationId, dto.Status);
            return Ok(result);
        }
    }

}
