using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.AreaReservation;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Domain.Interfaces;
using System.Net;

namespace ChamaOSindico.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAreaReservationRepository _areaReservationRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly ICondominalManagerRepository _condominalManagerRepository;

        public AreaService(IAreaRepository areaRepository,
            IAreaReservationRepository areaReservationRepository,
            IResidentRepository residentRepository,
            ICondominalManagerRepository condominalManagerRepository)
        {
            _areaRepository = areaRepository;
            _areaReservationRepository = areaReservationRepository;
            _residentRepository = residentRepository;
            _condominalManagerRepository = condominalManagerRepository;
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
                Status = areaReservationDTO.Status
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
         

        public async Task<ApiResponse<AreaReservationFullResponseDto>> GetAreaReservationByIdAsync(string id)
        {
            var reservation = await _areaReservationRepository.GetAreaReservationByIdAsync(id);

            if (reservation == null)
            {
                return ApiResponse<AreaReservationFullResponseDto>.ErrorResult("Reserva não encontrada.", HttpStatusCode.NotFound);
            }

            var createdByResident = await _residentRepository.GetResidentByUserIdAsync(reservation.CreatedByUserId);
            
            var answers = new List<AreaReservationAnswerResponseDto>();

            foreach (var answer in reservation.Answers)
            {
                var manager = await _condominalManagerRepository.GetCondominalManagerByUserIdAsync(answer.AnsweredByUserId);

                answers.Add(new AreaReservationAnswerResponseDto
                {
                    AreaReservationId = answer.AreaReservationId,
                    Answer = answer.Answer,
                    AnsweredByUserName = manager.Name,
                    AnsweredAt = answer.AnsweredAt
                });
            }

            var dto = new AreaReservationFullResponseDto
            {
                AreaName = reservation.AreaName,
                AreaId = reservation.AreaId,
                CreatedByUserName = createdByResident.Name,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                CreatedAt = reservation.CreatedAt,
                Status = reservation.Status,
                Answers = answers
            };

            return ApiResponse<AreaReservationFullResponseDto>.SuccessResult(dto, "Reserva Carregada");
        }

        public async Task<ApiResponse<string>> AddAnswerToAreaReservationAsync(AreaReservationAnswer answer)
        {
            var areaReservation = await _areaReservationRepository.GetAreaReservationByIdAsync(answer.AreaReservationId);

            if (areaReservation == null)
            {
                return ApiResponse<string>.ErrorResult("Reserva não encontrada para responder.", HttpStatusCode.NotFound);
            }

            await _areaReservationRepository.AddAnswerToAreaReservationAsync(answer);
            return ApiResponse<string>.SuccessResult(null, "Resposta adicionada com sucesso.");
        }

        public async Task<ApiResponse<string>> ChangeAreaReservationStatusAsync(string areaReservationId, AreaReservationStatusEnum newStatus)
        {
            var areaReservation = await _areaReservationRepository.GetAreaReservationByIdAsync(areaReservationId);

            if (areaReservation == null)
            {
                return ApiResponse<string>.ErrorResult("Reserva não encontrada.", HttpStatusCode.NotFound);
            }

            areaReservation.Status = newStatus;

            await _areaReservationRepository.UpdateAreaReservationAsync(areaReservationId, areaReservation);

            return ApiResponse<string>.SuccessResult(null, "Status da reserva alterado com sucesso.");
        }
    }
}
