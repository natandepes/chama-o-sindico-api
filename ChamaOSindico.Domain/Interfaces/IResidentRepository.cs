using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task CreateResidentAsync(Resident resident);
        Task AssignUserIdToResidentAsync(string residentId, string userId);

        Task<List<Resident>> GetAllResidentsAsync();
        Task<Resident?> GetResidentByIdAsync(string id);
        Task<List<Resident>> GetResidentsByUserIdAsync(List<string> userIds);
        Task<Resident?> GetResidentByUserIdAsync(string userId);
        Task UpdateResidentAsync(string id, Resident resident);
        Task DeleteResidentAsync(string id);
    }
}
