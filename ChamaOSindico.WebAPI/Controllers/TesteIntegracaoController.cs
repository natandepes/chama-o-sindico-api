using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TesteIntegracaoController : ControllerBase
    {
        // controller de teste de integração com o mongo
        
        private readonly TesteIntegracaoRepository _testeIntegracaoRepository;

        public TesteIntegracaoController(TesteIntegracaoRepository testeIntegracaoRepository)
        {
            _testeIntegracaoRepository = testeIntegracaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var testeIntegracao = await _testeIntegracaoRepository.GetAll();
            return Ok(testeIntegracao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var testeIntegracao = await _testeIntegracaoRepository.GetById(id);
            return Ok(testeIntegracao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TesteIntegracao testeIntegracao)
        {
            await _testeIntegracaoRepository.CreateAsync(testeIntegracao);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, TesteIntegracao testeIntegracao)
        {
            await _testeIntegracaoRepository.UpdateAsync(id, testeIntegracao);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _testeIntegracaoRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
