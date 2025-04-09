using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;

namespace ChamaOSindico.Application.Service
{

    public class VehicleService: IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleService(
            IVehicleRepository vehicleRepository
        ){
            _vehicleRepository = vehicleRepository;
        }

        public Task CreateVehicleAsync(Vehicle vehicle)
        {
            return _vehicleRepository.CreateVehicleAsync(vehicle);
        }

        public Task DeleteVehicleAsync(string idVehicle)
        {
            return _vehicleRepository.DeleteVehicleAsync(idVehicle);
        }

        public Task<List<Vehicle>> GetAllVehiclesByUserIdAsync(string userId)
        {
            return _vehicleRepository.GetAllVehiclesByUserIdAsync(userId);
        }

        public Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return _vehicleRepository.GetAllVehiclesAsync();
        }

        public Task<Vehicle> GetVehicleByIdAsync(string idVehicle)
        {
            return _vehicleRepository.GetVehicleByIdAsync(idVehicle);
        }

        public Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle)
        {
            return _vehicleRepository.UpdateVehicleAsync(idVehicle, vehicle);
        }
    }
}