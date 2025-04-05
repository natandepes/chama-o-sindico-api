using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class VehicleRepository
    {

        private readonly IMongoCollection<Vehicle> _context;

        public VehicleRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Vehicle>();
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(string idVehicle)
        {
            return await _context.Find(v => v.Id == idVehicle).FirstOrDefaultAsync();
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            await _context.InsertOneAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle)
        {
            await _context.ReplaceOneAsync(v => v.Id == idVehicle, vehicle);
        }

        public async Task DeleteVehicleAsync(string idVehicle)
        {
            await _context.DeleteOneAsync(v => v.Id == idVehicle);
        }
    }
}
