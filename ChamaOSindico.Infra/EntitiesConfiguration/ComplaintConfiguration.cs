using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    class ComplaintConfiguration : IMongoEntityConfiguration<Complaint>
    {
        public void Configure(BsonClassMap<Complaint> classMap)
        {
        classMap.AutoMap();
        
        classMap.MapIdMember(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        classMap.MapMember(c => c.Title)
            .SetIsRequired(true);
            
        classMap.MapMember(c => c.Description)
            .SetIsRequired(true);
            
        classMap.MapMember(c => c.ImageUrl)
            .SetIsRequired(false); 
            
        classMap.MapMember(c => c.Status)
            .SetIsRequired(true)
            .SetDefaultValue(ComplaintStatusEnum.Open);
            
        classMap.MapMember(c => c.CreatedAt)
            .SetIsRequired(true)
            .SetDefaultValue(DateTime.UtcNow);
            
        classMap.MapMember(c => c.ClosedAt)
            .SetIsRequired(false);         }
    }
}
