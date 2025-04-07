using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Context;
using ChamaOSindico.Infra.Interfaces;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class MongoTokenBlacklistRepository : ITokenBlackListRepository
    {
        private readonly IMongoCollection<BlacklistedToken> _blacklistCollection;

        public MongoTokenBlacklistRepository(MongoAppDbContext context)
        {
            _blacklistCollection = context.GetCollection<BlacklistedToken>();
        }

        public async Task AddTokenToBlackListAsync(string token, DateTime expiry)
        {
            var entry = new BlacklistedToken
            {
                Token = token,
                ExpiryDate = expiry
            };

            await _blacklistCollection.InsertOneAsync(entry);
        }

        public async Task<bool> IsTokenBlackListeAsync(string token)
        {
            var result = await _blacklistCollection
                .Find(x => x.Token == token)
                .FirstOrDefaultAsync();

            return result != null;
        }
    }
}
