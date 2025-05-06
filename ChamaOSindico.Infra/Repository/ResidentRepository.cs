using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly IMongoCollection<Resident> _context;

        public ResidentRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Resident>();
        }

        public async Task CreateResidentAsync(Resident resident)
        {
            await _context.InsertOneAsync(resident);
        }
        
        public async Task AssignUserIdToResidentAsync(string residentId, string userId)
        {
            var foundUser = await _context.Find(r => r.Id == residentId).FirstOrDefaultAsync();

            if (foundUser == null)
            {
                throw new Exception("Resident not found");
            }

            var update = Builders<Resident>.Update.Set(r => r.UserId, userId);
            await _context.UpdateOneAsync(r => r.Id == residentId, update);
        }

        public async Task<List<Resident>> GetAllResidentsAsync()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Resident?> GetResidentByIdAsync(string id)
        {
            return await _context.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Resident?> GetResidentByUserIdAsync(string userId)
        {
            return await _context.Find(r => r.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateResidentAsync(string id, Resident resident)
        {
            await _context.ReplaceOneAsync(r => r.Id == id, resident);
        }

        public async Task DeleteResidentAsync(string id)
        {
            await _context.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<List<Resident>> GetResidentsByUserIdAsync(List<string> userIds)
        {
            return await _context
                .Find(r => userIds.Contains(r.UserId))
                .ToListAsync();
        }
    }
}
