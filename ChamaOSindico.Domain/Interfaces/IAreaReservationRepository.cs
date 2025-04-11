using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Domain.Interfaces
{
    public  interface IAreaReservationRepository
    {
        Task<IEnumerable<AreaReservation>> GetAllAreaReservationsAsync();
        Task<IEnumerable<AreaReservation>> GetAllAreaReservationsByUserAsync(string residentId);
        Task<AreaReservation> GetAreaReservationByIdAsync(string id);
        Task SaveAreaReservationAsync(AreaReservation areaReservation);
        Task DeleteAreaReservationAsync(string id);
    }
}
