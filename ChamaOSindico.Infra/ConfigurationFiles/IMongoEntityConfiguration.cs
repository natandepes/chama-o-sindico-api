using MongoDB.Bson.Serialization;

namespace ChamaOSindico.Infra.ConfigurationFiles
{
    public interface IMongoEntityConfiguration<T>
    {
        void Configure(BsonClassMap<T> classMap);
    }
}
