using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Vehicles;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ChamaOSindico.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IResidentRepository _residentRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IResidentRepository residentRepository)
        {
            _vehicleRepository = vehicleRepository;
            _residentRepository = residentRepository;
        }

        public async Task<ApiResponse<List<VehicleDto>>> GetAllVehicles()
        {
            var listVehicles = await _vehicleRepository.GetAllVehiclesAsync();

            // Step 2: Get unique user IDs
            var userIds = listVehicles
                .Where(v => !string.IsNullOrEmpty(v.CreatedByUserId))
                .Select(v => v.CreatedByUserId)
                .Distinct()
                .ToList();

            // Step 3: Fetch all residents in a single DB query
            var residents = await _residentRepository
                .GetResidentsByUserIdAsync(userIds);

            // Step 4: Build lookup
            var userIdNameMap = residents.ToDictionary(r => r.UserId, r => r.Name);

            // Step 5: Translate to DTO with CreatedByUserName
            var listVehiclesDto = listVehicles.Select(vehicle =>
            {
                var dto = VehicleDto.TranslateTo(vehicle);
                dto.CreatedByUserId = vehicle.CreatedByUserId;
                userIdNameMap.TryGetValue(vehicle.CreatedByUserId, out var name);
                dto.CreatedByUserName = name;
                return dto;
            }).ToList();

            return ApiResponse<List<VehicleDto>>.SuccessResult(listVehiclesDto, null);
        }

        public async Task<ApiResponse<List<VehicleDto>>> GetAllVehiclesByUserId(string userId)
        {
            var listVehicles = await _vehicleRepository.GetAllVehiclesByUserIdAsync(userId);
            var listVehiclesDto = listVehicles.Select(lv =>
            {
                return VehicleDto.TranslateTo(lv);
            }).ToList();

            return ApiResponse<List<VehicleDto>>.SuccessResult(listVehiclesDto, null);
        }

        public async Task<ApiResponse<VehicleDto>> GetVehicleById(string id)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            var vehicleDto = VehicleDto.TranslateTo(vehicle);

            return ApiResponse<VehicleDto>.SuccessResult(vehicleDto, null);
        }

        public async Task<ApiResponse<string>> SaveVehicle(VehicleDto vehicleDto)
        {
            if (CarSpaceOccupied(vehicleDto.CarSpace, vehicleDto.Id).Result)
            {
                return ApiResponse<string>.ErrorResult("Esta vaga já está ocupada", HttpStatusCode.Conflict);
            }

            var vehicle = vehicleDto.TranslateBack();
            if (vehicleDto.Id.IsNullOrEmpty())
            {
                if (VerifyQuantityVehicles(vehicleDto.CreatedByUserId!).Result)
                {
                    return ApiResponse<string>.ErrorResult("Limite de Veículos já ultrapassado pelo morador", HttpStatusCode.Conflict);
                }

                await _vehicleRepository.CreateVehicleAsync(vehicle);
            }
            else
            {
                await _vehicleRepository.UpdateVehicleAsync(vehicle.Id!, vehicle);
            }

            return ApiResponse<string>.SuccessResult(null, "null");
        }

        public async Task<ApiResponse<string>> DeleteVehicle(string id)
        {
            await _vehicleRepository.DeleteVehicleAsync(id);
            return ApiResponse<string>.SuccessResult(null, "null");
        }

        private async Task<bool> VerifyQuantityVehicles(string userId)
        {
            var listVehicles = await _vehicleRepository.GetAllVehiclesByUserIdAsync(userId);

            return listVehicles.Count >= 2;
        }

        private async Task<bool> CarSpaceOccupied(int carSpace, string? id)
        {
            var vehicle = await _vehicleRepository.CarSpaceOccupied(carSpace);

            return vehicle is not null && vehicle.Id != id;
        }
    }
}
