using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Vehicles;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IVehicleService
    {
        public Task<ApiResponse<List<VehicleDto>>> GetAllVehicles();
        public Task<ApiResponse<List<VehicleDto>>> GetAllVehiclesByUserId(string userId);
        public Task<ApiResponse<VehicleDto>> GetVehicleById(string id);
        public Task<ApiResponse<string>> SaveVehicle(VehicleDto vehicleDto);
        public Task<ApiResponse<string>> DeleteVehicle(string id);
    }
}
