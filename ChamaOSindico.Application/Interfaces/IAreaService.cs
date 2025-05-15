using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.AreaReservation;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;

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
        Task<ApiResponse<AreaReservationFullResponseDto>> GetAreaReservationByIdAsync(string id);
        Task<ApiResponse<string>> SaveAreaReservationAsync(AreaReservationDTO areaReservation);
        Task<ApiResponse<string>> DeleteAreaReservationAsync(string id);
        Task<ApiResponse<string>> AddAnswerToAreaReservationAsync(AreaReservationAnswer answer);
        Task<ApiResponse<string>> ChangeAreaReservationStatusAsync(string areaReservationId, AreaReservationStatusEnum newStatus);
    }
}
