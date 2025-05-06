using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface ICondominalManagerRepository
    {
        Task<CondominalManager?> GetCondominalManagerByEmail(string email);
        Task<CondominalManager?> GetCondominalManagerByUserIdAsync(string userId);
        Task<CondominalManager?> GetCurrentCondominalManager();
        Task AssignUserIdToCondominalManagerAsync(string condominalManagerId, string userId);
        Task CreateCondominalManagerAsync(CondominalManager condominalManager);
        Task UpdateResidentAsync(string id, CondominalManager condominalManager);
    }
}
