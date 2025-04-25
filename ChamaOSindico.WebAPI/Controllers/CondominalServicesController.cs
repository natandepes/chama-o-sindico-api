using Microsoft.AspNetCore.Mvc;
using MediatR;
using ChamaOSindico.WebAPI.Payloads;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/condominal-services")]
    public class CondominalServicesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] CreateCondominalServicePayload payload) 
            => await mediator.Send(payload.AsCommand());
    }
}
