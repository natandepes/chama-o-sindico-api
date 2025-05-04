using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
