using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class ResidentConfiguration : IMongoEntityConfiguration<Resident>
    {
        public void Configure(BsonClassMap<Resident> classMap)
        {
            classMap.AutoMap();

            classMap.MapMember(r => r.ApartmentNumber)
                .SetIsRequired(true);
        }
    }
}
