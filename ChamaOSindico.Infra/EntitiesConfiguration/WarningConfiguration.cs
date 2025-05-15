using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class WarningConfiguration : IMongoEntityConfiguration<Warning>
    {
        public void Configure(BsonClassMap<Warning> classMap)
        {
            classMap.AutoMap();
            
            classMap.MapMember(w => w.Title)
                .SetIsRequired(true);
            
            classMap.MapMember(w => w.Description)
                .SetIsRequired(true);
            
            classMap.MapMember(w => w.CreatedAt)
                .SetIsRequired(true);
            
            classMap.MapMember(w => w.TargetType)
                .SetIsRequired(true);
            
            classMap.MapMember(w => w.ResidentId)
                .SetIsRequired(false);
            
            classMap.MapMember(w => w.ResidentUserId)
                .SetIsRequired(false);
        }
    }
}
