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
    public class AreaRepository : IAreaRepository
    {
        private readonly IMongoCollection<Area> _context;

        public AreaRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Area>();
        }


        public async Task DeleteAreaAsync(string id)
        {
            await _context.DeleteOneAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Area> GetAreaByIdAsync(string id)
        {
            return await _context.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveAreaAsync(Area area)
        {
            if(string.IsNullOrEmpty(area.Id))
            {
                await _context.InsertOneAsync(area);
            }
            else
            {
                await _context.ReplaceOneAsync(a => a.Id == area.Id, area);
            }
        }
    }
}
