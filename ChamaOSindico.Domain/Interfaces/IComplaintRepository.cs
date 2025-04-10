using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{

        public interface IComplaintRepository
        {
                Task<List<Complaint>> GetAllComplaintsAsync();
                Task<Complaint> GetComplaintByIdAsync(string id);
                Task CreateComplaintAsync(Complaint complaint);
                Task UpdateComplaintAsync(string idcomplaint, Complaint complaint);
                Task DeleteComplaintAsync(string idcomplaint);
        }
}
