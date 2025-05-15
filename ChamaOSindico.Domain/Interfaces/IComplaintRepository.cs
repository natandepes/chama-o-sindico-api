using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Domain.Interfaces
{
        public interface IComplaintRepository
        {
                Task<List<Complaint>> GetAllComplaintsAsync();
                Task<List<Complaint>> GetAllComplaintsAsyncByUserId(string idUser);
                Task<Complaint> GetComplaintByIdAsync(string id);
                Task CreateComplaintAsync(Complaint complaint);
                Task UpdateComplaintAsync(string idcomplaint, Complaint complaint);
                Task DeleteComplaintAsync(string idcomplaint);
                Task AddAnswerToComplaintAsync(ComplaintAnswer answer);
        }
}
