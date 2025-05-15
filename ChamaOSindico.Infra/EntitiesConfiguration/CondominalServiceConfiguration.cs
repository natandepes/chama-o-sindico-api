using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson.Serialization;
namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class CondominalServiceConfiguration : IMongoEntityConfiguration<CondominalService>
    {
        public void Configure(BsonClassMap<CondominalService> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(c => c.Title)
                .SetIsRequired(true);

            classMap.MapMember(c => c.ProviderPhotoUrl)
                .SetIsRequired(true);

            classMap.MapMember(c => c.ProviderName)
                .SetIsRequired(true);

            classMap.MapMember(c => c.Cellphone)
                .SetIsRequired(true);

            classMap.MapMember(c => c.Description)
                .SetIsRequired(true);
        }
    }
}
