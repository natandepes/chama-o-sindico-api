using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;

namespace ChamaOSindico.Application.Service
{

    public class ComplaintService: IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;


        public ComplaintService(
            IComplaintRepository complaintRepository
        ){
            _complaintRepository = complaintRepository;
        }

        public Task CreateComplaint(Complaint complaint)
        {
            return _complaintRepository.CreateComplaint(complaint);
        }

        public Task DeleteComplaintAsync(string idComplaint)
        {
            return _complaintRepository.DeleteComplaintAsync(idComplaint);
        }

        public Task<List<Complaint>> GetAllComplaints()
        {
            return _complaintRepository.GetAllComplaints();
        }

        public Task<Complaint> GetComplaintById(string idComplaint)
        {
            return _complaintRepository.GetComplaintById(idComplaint);
        }

        public Task UpdateComplaintAsync(string idComplaint, Complaint complaint)
        {
            return _complaintRepository.UpdateComplaintAsync(idComplaint, complaint);
        }
    }
}