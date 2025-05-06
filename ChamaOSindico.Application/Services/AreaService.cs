using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.AreaReservation;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Domain.Interfaces;

namespace ChamaOSindico.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAreaReservationRepository _areaReservationRepository;
        private readonly IResidentRepository _residentRepository;

        public AreaService(IAreaRepository areaRepository,
            IAreaReservationRepository areaReservationRepository,
            IResidentRepository residentRepository)
        {
            _areaRepository = areaRepository;
            _areaReservationRepository = areaReservationRepository;
            _residentRepository = residentRepository;
        }

        public async Task<ApiResponse<string>> DeleteAreaAsync(string id)
        {
            await _areaRepository.DeleteAreaAsync(id);

            return ApiResponse<string>.SuccessResult(null, "Área Deletada com Sucesso");
        }

        public async Task<ApiResponse<List<AreaDTO>>> GetAllAreasAsync()
        {
            var areas = await _areaRepository.GetAllAreasAsync();
            var areasDTO = areas.ToList().Select(a =>
            {
                return new AreaDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Capacity = a.Capacity,
                    Status = a.Status,
                    OpenTime = a.OpenTime,
                    CloseTime = a.CloseTime
                };
            }).ToList();

            return ApiResponse<List<AreaDTO>>.SuccessResult(areasDTO, null);
        }

        public async Task<ApiResponse<AreaDTO>> GetAreaByIdAsync(string id)
        {
            var area = await _areaRepository.GetAreaByIdAsync(id);

            var areaDto = new AreaDTO
            {
                Id = area.Id,
                Name = area.Name,
                Description = area.Description,
                Capacity = area.Capacity,
                Status = area.Status,
                OpenTime = area.OpenTime,
                CloseTime = area.CloseTime
            };

            return ApiResponse<AreaDTO>.SuccessResult(areaDto, null);
        }

        public async Task<ApiResponse<string>> SaveAreaAsync(AreaDTO areaDto)
        {
            var area = new Area
            {
                Id = areaDto.Id ?? "",
                Name = areaDto.Name,
                Description = areaDto.Description,
                Capacity = areaDto.Capacity,
                Status = areaDto.Status,
                OpenTime = areaDto.OpenTime,
                CloseTime = areaDto.CloseTime
            };

            await _areaRepository.SaveAreaAsync(area);

            return ApiResponse<string>.SuccessResult(null, "Área Salva com Sucesso");
        }

        public async Task<ApiResponse<string>> SaveAreaReservationAsync(AreaReservationDTO areaReservationDTO)
        {
            var areaReservation = new AreaReservation
            {
                Id = areaReservationDTO.Id ?? "",
                AreaId = areaReservationDTO.AreaId,
                AreaName = areaReservationDTO.AreaName,
                CreatedByUserId = areaReservationDTO.CreatedByUserId,
                StartDate = areaReservationDTO.StartDate,
                EndDate = areaReservationDTO.EndDate,
                CreatedAt = DateTime.Now,
                Status = (AreaReservationStatusEnum)Enum.Parse(typeof(AreaReservationStatusEnum), areaReservationDTO.Status)
            };

            await _areaReservationRepository.SaveAreaReservationAsync(areaReservation);

            return ApiResponse<string>.SuccessResult(null, "Reserva Salva com Sucesso");
        }

        public async Task<ApiResponse<string>> DeleteAreaReservationAsync(string id)
        {
            await _areaReservationRepository.DeleteAreaReservationAsync(id);

            return ApiResponse<string>.SuccessResult(null, "Reserva Deletada com Sucesso");
        }

        public async Task<ApiResponse<List<AreaReservationResponseDto>>> GetAllAreaReservationsAsync()
        {
            var reservations = await _areaReservationRepository.GetAllAreaReservationsAsync();

            var areaReservationDto = new List<AreaReservationResponseDto>();

            foreach (var reservation in reservations)
            {
                var resident = await _residentRepository.GetResidentByUserIdAsync(reservation.CreatedByUserId);

                var areaReservation = new AreaReservationResponseDto
                {
                    Id = reservation.Id,
                    AreaName = reservation.AreaName,
                    CreatedByUserName = resident.Name,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate
                };

                areaReservationDto.Add(areaReservation);
            }

            return ApiResponse<List<AreaReservationResponseDto>>.SuccessResult(areaReservationDto, null);
        }


        public async Task<ApiResponse<List<AreaReservation>>> GetAllAreaReservationsByUserAsync(string residentId)
        {
            var reservations = await _areaReservationRepository.GetAllAreaReservationsByUserAsync(residentId);

            return ApiResponse<List<AreaReservation>>.SuccessResult(reservations.ToList(), null);
        }
         

        public async Task<ApiResponse<AreaReservation>> GetAreaReservationByIdAsync(string id)
        {
            var reservations = await _areaReservationRepository.GetAreaReservationByIdAsync(id);

            return ApiResponse<AreaReservation>.SuccessResult(reservations, null);
        }
    }
}
