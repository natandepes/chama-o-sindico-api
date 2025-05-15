using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IWarningRepository
    {
        Task CreateWarningAsync(Warning warning);
        Task<List<Warning>> GetAllWarningsAsync();
        Task DeleteWarningAsync(string id);
    }
}
