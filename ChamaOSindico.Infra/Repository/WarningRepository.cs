using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class WarningRepository : IWarningRepository
    {
        private readonly IMongoCollection<Warning> _context;

        public WarningRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Warning>();
        }

        public async Task CreateWarningAsync(Warning warning)
        {
            await _context.InsertOneAsync(warning);
        }

        public async Task DeleteWarningAsync(string id)
        {
            await _context.DeleteOneAsync(w => w.Id == id);
        }

        public async Task<List<Warning>> GetAllWarningsAsync()
        {
            var sort = Builders<Warning>.Sort.Descending(w => w.CreatedAt);
            return await _context.Find(_ => true).Sort(sort).ToListAsync();
        }
    }
}
