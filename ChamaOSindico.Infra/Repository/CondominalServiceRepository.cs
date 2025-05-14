using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class CondominalServiceRepository : ICondominalServiceRepository
    {
        private readonly IMongoCollection<CondominalService> _context;

        public CondominalServiceRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<CondominalService>();
        }

        public async Task<string> CreateCondominalServiceAsync(CondominalService condominalService)
        {
            await _context.InsertOneAsync(condominalService);
            return condominalService.Id!;
        }
    }
}
