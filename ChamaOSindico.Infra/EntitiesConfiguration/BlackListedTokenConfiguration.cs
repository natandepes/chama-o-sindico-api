using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class BlackListedTokenConfiguration : IMongoEntityConfiguration<BlacklistedToken>
    {
        public void Configure(BsonClassMap<BlacklistedToken> classMap)
        {
            classMap.AutoMap();
            
            classMap.SetIgnoreExtraElements(true);
            
            classMap.MapIdMember(c => c.Id)
                .SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        }
    }
}
