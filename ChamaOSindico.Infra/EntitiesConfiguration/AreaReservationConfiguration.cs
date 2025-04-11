using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class AreaReservationConfiguration : IMongoEntityConfiguration<AreaReservation>
    {
        public void Configure(BsonClassMap<AreaReservation> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(c => c.AreaId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true);
            classMap.MapMember(c => c.CreatedByUserId)
                .SetIsRequired(true);
            classMap.MapMember(c => c.StartDate)
                .SetIsRequired(true);
            classMap.MapMember(c => c.EndDate)
                .SetIsRequired(true);
            classMap.MapMember(c => c.CreatedAt)
                .SetIsRequired(true);
            classMap.MapMember(c => c.Status)
                .SetIsRequired(true);
        }
    }
}
