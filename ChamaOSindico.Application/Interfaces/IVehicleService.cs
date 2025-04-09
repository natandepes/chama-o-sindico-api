using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Application.Interfaces
{

    public interface IVehicleService
    {
                Task<List<Vehicle>> GetAllVehiclesAsync();
                Task<Vehicle> GetVehicleByIdAsync(string id);
                Task CreateVehicleAsync(Vehicle vehicle);
                Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle);
                Task DeleteVehicleAsync(string idVehicle);
        
    }
}