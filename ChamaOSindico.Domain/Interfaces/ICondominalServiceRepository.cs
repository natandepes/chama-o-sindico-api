using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface ICondominalServiceRepository
    {
        Task<List<CondominalService>> GetAllServices();
        Task<CondominalService> GetServiceById(string id);
        public Task<string> CreateServiceAsync(CondominalService condominalService);
        public Task<string> UpdateServiceAsync(string idService, CondominalService condominalService);
        public Task DeleteVehicleAsync(string id);
    }
}
