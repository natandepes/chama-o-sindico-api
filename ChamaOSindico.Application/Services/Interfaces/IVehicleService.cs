

using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Application.Service
{

    public interface IVehicleService
    {
                Task<List<Vehicle>> GetAllVehicles();
                Task<Vehicle> GetVehicleById(string id);
                Task CreateVehicle(Vehicle vehicle);
                Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle);
                Task DeleteVehicleAsync(string idVehicle);
        
    }
}