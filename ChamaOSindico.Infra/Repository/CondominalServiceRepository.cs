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

        public async Task<List<CondominalService>> GetAllServices()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<CondominalService> GetServiceById(string id)
        {
            return await _context.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> CreateServiceAsync(CondominalService condominalService)
        {
            await _context.InsertOneAsync(condominalService);

            return condominalService.Id!;
        }

        public async Task<string> UpdateServiceAsync(string idService, CondominalService condominalService)
        {
            await _context.ReplaceOneAsync(v => v.Id == idService, condominalService);

            return condominalService.Id!;
        }

        public async Task DeleteVehicleAsync(string id)
        {
            await _context.DeleteOneAsync(v => v.Id == id);
        }
    }
}
