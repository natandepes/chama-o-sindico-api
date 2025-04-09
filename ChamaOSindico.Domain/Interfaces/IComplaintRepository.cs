using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{

        public interface IComplaintRepository
        {
                Task<List<Complaint>> GetAllComplaints();
                Task<Complaint> GetComplaintById(string id);
                Task CreateComplaint(Complaint complaint);
                Task UpdateComplaintAsync(string idcomplaint, Complaint complaint);
                Task DeleteComplaintAsync(string idcomplaint);
        }
}
