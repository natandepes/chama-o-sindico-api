using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ChamaOSindico.Infra.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
