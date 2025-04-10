using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class AreaReservationConfiguration : IMongoEntityConfiguration<AreaReservation>
    {
        public void Configure(BsonClassMap<AreaReservation> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapIdMember(c => c.Id)
                .SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            classMap.MapMember(c => c.AreaId)
                .SetIsRequired(true);
            classMap.MapMember(c => c.ResidentId)
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
