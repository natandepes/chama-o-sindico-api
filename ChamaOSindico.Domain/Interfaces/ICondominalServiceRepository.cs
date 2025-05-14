using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface ICondominalServiceRepository
    {
        Task<string> CreateCondominalServiceAsync(CondominalService condominalService);
    }
}
