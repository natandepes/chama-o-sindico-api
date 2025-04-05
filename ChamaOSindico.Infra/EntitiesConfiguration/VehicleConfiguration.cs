using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    class VeicleConfiguration : IMongoEntityConfiguration<Vehicle>
    {
        // arquivo de configuração de entidade de teste de integração com o mongo

        public void Configure(BsonClassMap<Vehicle> classMap)
        {
            classMap.AutoMap();

            classMap.MapIdMember(v => v.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            
            classMap.MapMember(v => v.LicensePlate)
                .SetIsRequired(true)
                .SetElementName("license_plate"); 
            
            classMap.MapMember(v => v.Model)
                .SetIsRequired(true);
            
            classMap.MapMember(v => v.Color)
                .SetIsRequired(true)
                .SetSerializer(new EnumSerializer<VehicleColorEnum>(BsonType.String)); 
            
            classMap.MapMember(v => v.VehicleType)
                .SetIsRequired(true)
                .SetSerializer(new EnumSerializer<VehicleTypeEnum>(BsonType.String)); 
        }
    }
}
