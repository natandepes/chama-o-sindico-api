using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAreaReservationRepository _areaReservationRepository;

        public AreaService(IAreaRepository areaRepository,
            IAreaReservationRepository areaReservationRepository)
        {
            _areaRepository = areaRepository;
            _areaReservationRepository = areaReservationRepository;
        }

        public async Task DeleteAreaAsync(string id)
        {
            await _areaRepository.DeleteAreaAsync(id);
        }

        public async Task<IEnumerable<AreaDTO>> GetAllAreasAsync()
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

            return areasDTO;
        }

        public async Task<AreaDTO> GetAreaByIdAsync(string id)
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

            return areaDto;
        }

        public async Task SaveAreaAsync(AreaDTO areaDto)
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
        }

        public async Task SaveAreaReservationAsync(AreaReservationDTO areaReservationDTO)
        {
            var areaReservation = new AreaReservation
            {
                Id = areaReservationDTO.Id ?? "",
                AreaId = areaReservationDTO.AreaId,
                CreatedByUserId = areaReservationDTO.CreatedByUserId,
                StartDate = areaReservationDTO.StartDate,
                EndDate = areaReservationDTO.EndDate,
                Status = areaReservationDTO.Status
            };

            await _areaReservationRepository.SaveAreaReservationAsync(areaReservation);
        }

        public async Task DeleteAreaReservationAsync(string id)
        {
            await _areaReservationRepository.DeleteAreaReservationAsync(id);
        }

        public async Task<IEnumerable<AreaReservation>> GetAllAreaReservationsAsync()
        => await _areaReservationRepository.GetAllAreaReservationsAsync();


        public async Task<IEnumerable<AreaReservation>> GetAllAreaReservationsByUserAsync(string residentId)
        => await _areaReservationRepository.GetAllAreaReservationsByUserAsync(residentId);

        public async Task<AreaReservation> GetAreaReservationByIdAsync(string id)
        => await _areaReservationRepository.GetAreaReservationByIdAsync(id);
    }
}
