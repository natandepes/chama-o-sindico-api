using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> GetAllAreasAsync();
        Task<Area> GetAreaByIdAsync(string id);
        Task SaveAreaAsync(Area area);
        Task DeleteAreaAsync(string id);
    }
}
