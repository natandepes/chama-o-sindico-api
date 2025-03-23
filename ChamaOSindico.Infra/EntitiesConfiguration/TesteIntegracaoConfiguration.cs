using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    class TesteIntegracaoConfiguration : IMongoEntityConfiguration<TesteIntegracao>
    {
        // arquivo de configuração de entidade de teste de integração com o mongo

        public void Configure(BsonClassMap<TesteIntegracao> classMap)
        {
            classMap.AutoMap();

            classMap.MapIdMember(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            classMap.MapMember(c => c.Name)
                .SetIsRequired(true);
        }
    }
}
