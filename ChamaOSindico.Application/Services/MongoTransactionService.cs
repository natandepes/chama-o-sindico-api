using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Application.Services
{
    public class MongoTransactionService : ITransactionService
    {
        private readonly IMongoClient _mongoClient;
        private readonly MongoAppDbContext _context;

        public MongoTransactionService(IMongoClient mongoClient, MongoAppDbContext context)
        {
            _mongoClient = mongoClient;
            _context = context;
        }

        public async Task ExecuteTransactionAsync(Func<Task> action)
        {
            using var session = await _mongoClient.StartSessionAsync();
            session.StartTransaction();

            try
            {
                await action();
                await session.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
