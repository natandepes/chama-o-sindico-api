﻿using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task CreateResidentAsync(Resident resident);
        Task AssignUserIdToResidentAsync(string residentId, string userId);

        Task<List<Resident>> GetAllResidentsAsync();
        Task<Resident?> GetResidentByIdAsync(string id);
        Task UpdateResidentAsync(string id, Resident resident);
        Task DeleteResidentAsync(string id);
    }
}
