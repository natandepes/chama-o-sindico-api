using ChamaOSindico.Application.DTOs;
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
        Task<IEnumerable<AreaDTO>> GetAllAreasAsync();
        Task<AreaDTO> GetAreaByIdAsync(string id);
        Task SaveAreaAsync(AreaDTO areaDto);
        Task DeleteAreaAsync(string id);
        Task<IEnumerable<AreaReservation>> GetAllAreaReservationsAsync();
        Task<IEnumerable<AreaReservation>> GetAllAreaReservationsByUserAsync(string userId);
        Task<AreaReservation> GetAreaReservationByIdAsync(string id);
        Task SaveAreaReservationAsync(AreaReservationDTO areaReservation);
        Task DeleteAreaReservationAsync(string id);
    }
}
