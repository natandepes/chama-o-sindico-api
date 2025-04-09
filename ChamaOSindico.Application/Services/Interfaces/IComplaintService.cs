

using ChamaOSindico.Domain.Entities;

namespace ChamaOSindico.Application.Service
{

    public interface IComplaintService
    {
                Task<List<Complaint>> GetAllComplaints();
                Task<Complaint> GetComplaintById(string id);
                Task CreateComplaint(Complaint complaint);
                Task UpdateComplaintAsync(string idcomplaint, Complaint complaint);
                Task DeleteComplaintAsync(string idcomplaint);
        
    }
}