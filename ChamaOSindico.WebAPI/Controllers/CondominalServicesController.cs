using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Application.DTOs;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CondominalServicesController : ControllerBase
    {
        private readonly ICondominalServiceService _condominalServiceService;
        public CondominalServicesController(ICondominalServiceService condominalServiceService)
        {
            _condominalServiceService = condominalServiceService;
        }

        [HttpPost(nameof(SaveService))]
        public async Task<IActionResult> SaveService([FromBody] CondominalServiceDTO service)
        {
            var response = await _condominalServiceService.SaveService(service);

            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetAllServices))]
        public async Task<IActionResult> GetAllServices()
        {
            var response = await _condominalServiceService.GetAllServices();

            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet(nameof(GetServiceById) + "/{id}")]
        public async Task<IActionResult> GetServiceById(string id)
        {
            var response = await _condominalServiceService.GetServiceById(id);

            if (response == null)
            {
                return NotFound("Condominal Service not found");
            }

            return StatusCode(Response.StatusCode, response);
        }
    }
}
