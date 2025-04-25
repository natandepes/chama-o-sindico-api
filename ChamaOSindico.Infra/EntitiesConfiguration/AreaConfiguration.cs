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
    public class AreaConfiguration : IMongoEntityConfiguration<Area>
    {
        public void Configure(BsonClassMap<Area> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(c => c.Name)
                .SetIsRequired(true);
            classMap.MapMember(c => c.Description)
                .SetIsRequired(true);
            classMap.MapMember(c => c.Capacity)
                .SetIsRequired(true);
            classMap.MapMember(c => c.Status)
                .SetIsRequired(true);
            classMap.MapMember(c => c.OpenTime)
                .SetIsRequired(true);
            classMap.MapMember(c => c.CloseTime)
                .SetIsRequired(true);
        }
    }
}
