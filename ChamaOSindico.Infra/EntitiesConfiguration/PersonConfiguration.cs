﻿using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.ConfigurationFiles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    public class PersonConfiguration : IMongoEntityConfiguration<Person>
    {
        public void Configure(BsonClassMap<Person> classMap)
        {
            classMap.AutoMap();
            
            classMap.MapMember(p => p.Name)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.Email)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.Phone)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.Rg)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.Cpf)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.BirthDate)
                .SetIsRequired(true);
            
            classMap.MapMember(p => p.UserId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(false);
        }
    }
}
