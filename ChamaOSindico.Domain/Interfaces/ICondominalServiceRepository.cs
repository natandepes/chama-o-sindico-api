using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface ICondominalServiceRepository
    {
        Task<List<CondominalService>> GetAllServices();
        Task<CondominalService> GetServiceById(string id);
        public Task<string> CreateVehicleAsync(CondominalService condominalService);
        public Task<string> UpdateVehicleAsync(string idService, CondominalService condominalService);
    }
}
