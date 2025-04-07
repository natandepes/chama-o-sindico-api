using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    class TesteIntegracaoConfiguration : IMongoEntityConfiguration<TesteIntegracao>
    {
        // arquivo de configuração de entidade de teste de integração com o mongo

        public void Configure(BsonClassMap<TesteIntegracao> classMap)
        {
            classMap.AutoMap();

            classMap.MapMember(c => c.Name)
                .SetIsRequired(true);
        }
    }
}
