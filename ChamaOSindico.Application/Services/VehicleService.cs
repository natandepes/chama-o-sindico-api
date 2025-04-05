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

        public Task CreateVehicle(Vehicle vehicle)
        {
            return _vehicleRepository.CreateVehicle(vehicle);
        }

        public Task DeleteVehicleAsync(string idVehicle)
        {
            return _vehicleRepository.DeleteVehicleAsync(idVehicle);
        }

        public Task<List<Vehicle>> GetAllVehicles()
        {
            return _vehicleRepository.GetAllVehicles();
        }

        public Task<Vehicle> GetVehicleById(string idVehicle)
        {
            return _vehicleRepository.GetVehicleById(idVehicle);
        }

        public Task UpdateVehicleAsync(string idVehicle, Vehicle vehicle)
        {
            return _vehicleRepository.UpdateVehicleAsync(idVehicle, vehicle);
        }
    }
}