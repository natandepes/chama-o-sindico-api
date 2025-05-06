using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.AreaReservation;
using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IAreaService
    {
        Task<ApiResponse<List<AreaDTO>>> GetAllAreasAsync();
        Task<ApiResponse<AreaDTO>> GetAreaByIdAsync(string id);
        Task<ApiResponse<string>> SaveAreaAsync(AreaDTO areaDto);
        Task<ApiResponse<string>> DeleteAreaAsync(string id);
        Task<ApiResponse<List<AreaReservationResponseDto>>> GetAllAreaReservationsAsync();
        Task<ApiResponse<List<AreaReservation>>> GetAllAreaReservationsByUserAsync(string userId);
        Task<ApiResponse<AreaReservation>> GetAreaReservationByIdAsync(string id);
        Task<ApiResponse<string>> SaveAreaReservationAsync(AreaReservationDTO areaReservation);
        Task<ApiResponse<string>> DeleteAreaReservationAsync(string id);
    }
}
