using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task CreateResidentAsync(Resident resident);
        Task AssignUserIdToResidentAsync(string residentId, string userId);
    }
}
