using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{


        public interface IVehicleRepository
        {
                Task<List<Vehicle>> GetAllVehicles();
                Task<Vehicle> GetVehicleById(string idVehicle);
                Task CreateVehicle(Vehicle vehicle);
                Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle);
                Task DeleteVehicleAsync(string idVehicle);
        }
}
