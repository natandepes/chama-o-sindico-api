using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _repository.CreateAsync(product);
            return Ok();
        }
    }
}
