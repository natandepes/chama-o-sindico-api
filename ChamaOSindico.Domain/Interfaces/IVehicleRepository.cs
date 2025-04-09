using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{


        public interface IVehicleRepository
        {
                Task<List<Vehicle>> GetAllVehiclesAsync();
                Task<Vehicle> GetVehicleByIdAsync(string idVehicle);
                Task CreateVehicleAsync(Vehicle vehicle);
                Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle);
                Task DeleteVehicleAsync(string idVehicle);
        }
}
