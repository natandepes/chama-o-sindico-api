using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class CondominalManagerConfiguration : IMongoEntityConfiguration<CondominalManager>
    {
        public void Configure(BsonClassMap<CondominalManager> classMap)
        {
            classMap.AutoMap();

            classMap.MapMember(cm => cm.Salary)
                .SetIsRequired(true);

            classMap.MapMember(cm => cm.IsResident)
                .SetIsRequired(true);
        }
    }
}
