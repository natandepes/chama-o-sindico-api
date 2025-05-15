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
    public class ServiceCommentConfiguration : IMongoEntityConfiguration<ServiceComment>
    {
        public void Configure(BsonClassMap<ServiceComment> classMap)
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(c => c.CondominalServiceId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true);
            classMap.MapMember(c => c.Comment)
                .SetIsRequired(true);
            classMap.MapMember(c => c.CommentByUserId)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true);
            classMap.MapMember(c => c.CommentByUserName)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true);
            classMap.MapMember(c => c.CreatedAt)
                .SetIsRequired(true);
        }
    }
}
