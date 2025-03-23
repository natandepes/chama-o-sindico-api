using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class TesteIntegracaoRepository
    {
        // repository de teste de integração com o mongo

        private readonly IMongoCollection<TesteIntegracao> _testeIntegracao;

        public TesteIntegracaoRepository(MongoAppDbContext context)
        {
            _testeIntegracao = context.GetCollection<TesteIntegracao>();
        }

        public async Task<List<TesteIntegracao>> GetAll()
        {
            return await _testeIntegracao.Find(_ => true).ToListAsync();
        }

        public async Task<TesteIntegracao> GetById(string id)
        {
            return await _testeIntegracao.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TesteIntegracao testeIntegracao)
        {
            await _testeIntegracao.InsertOneAsync(testeIntegracao);
        }

        public async Task UpdateAsync(string id, TesteIntegracao testeIntegracao)
        {
            await _testeIntegracao.ReplaceOneAsync(t => t.Id == id, testeIntegracao);
        }

        public async Task DeleteAsync(string id)
        {
            await _testeIntegracao.DeleteOneAsync(t => t.Id == id);
        }
    }
}
