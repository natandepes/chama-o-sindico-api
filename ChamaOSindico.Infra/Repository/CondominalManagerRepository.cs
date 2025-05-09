using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class CondominalManagerRepository : ICondominalManagerRepository
    {
        private readonly IMongoCollection<CondominalManager> _context;

        public CondominalManagerRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<CondominalManager>();
        }

        public async Task<CondominalManager?> GetCondominalManagerByEmail(string email)
        {
            var filter = Builders<CondominalManager>.Filter.Eq(cm => cm.Email, email);
            return await _context.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateCondominalManagerAsync(CondominalManager condominalManager)
        {
            await _context.InsertOneAsync(condominalManager);
        }

        public async Task AssignUserIdToCondominalManagerAsync(string condominalManagerId, string userId)
        {
            var foundCondominalManager = await _context.Find(r => r.Id == condominalManagerId).FirstOrDefaultAsync();

            if (foundCondominalManager == null)
            {
                throw new Exception("Síndico não encontrado");
            }

            var update = Builders<CondominalManager>.Update.Set(r => r.UserId, userId);
            await _context.UpdateOneAsync(r => r.Id == condominalManagerId, update);
        }

        public async Task<CondominalManager?> GetCondominalManagerByUserIdAsync(string userId)
        {

            return await _context.Find(r => r.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<CondominalManager> GetCurrentCondominalManager()
        {
            return await _context.Find(_ => true).FirstOrDefaultAsync();
        }

        public async Task UpdateResidentAsync(string id, CondominalManager condominalManager)
        {
            await _context.ReplaceOneAsync(r => r.Id == id, condominalManager);
        }
    }
}
