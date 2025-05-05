using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ApiResponse<List<VehicleDto>>> GetAllVehicles()
        {
            var listVehicles = await _vehicleRepository.GetAllVehiclesAsync();
            var listVehiclesDto = listVehicles.Select(lv =>
            {
                return VehicleDto.TranslateTo(lv);
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
