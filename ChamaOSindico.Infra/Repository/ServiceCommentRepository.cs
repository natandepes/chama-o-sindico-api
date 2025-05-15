using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Infra.Repository
{
    public class ServiceCommentRepository : IServiceCommentRepository
    {
        private readonly IMongoCollection<ServiceComment> _context;

        public ServiceCommentRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<ServiceComment>();
        }

        public async Task CreateServiceComment(ServiceComment serviceComment)
        {
            await _context.InsertOneAsync(serviceComment);
        }
        public async Task<List<ServiceComment>> GetServiceComments(string serviceId)
        {
            return await _context.Find(s => s.CondominalServiceId == serviceId).ToListAsync();
        }
    }
}
