using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class UserConfiguration : IMongoEntityConfiguration<User>
    {
        public void Configure(BsonClassMap<User> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            
            classMap.MapIdMember(c => c.Id)
                .SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            classMap.MapMember(c => c.Role)
                .SetSerializer(new EnumSerializer<UserRoleEnum>(BsonType.String))
                .SetIsRequired(true);

            classMap.MapMember(c => c.Email)
                .SetIsRequired(true);

            classMap.MapMember(c => c.PasswordHash)
                .SetIsRequired(true);

            classMap.MapMember(c => c.PersonId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true);
        }
    }
}
