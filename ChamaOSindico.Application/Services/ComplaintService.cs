using ChamaOSindico.Application.Interfaces;
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

        public Task CreateComplaintAsync(Complaint complaint)
        {
            return _complaintRepository.CreateComplaintAsync(complaint);
        }

        public Task DeleteComplaintAsync(string idComplaint)
        {
            return _complaintRepository.DeleteComplaintAsync(idComplaint);
        }

        public Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return _complaintRepository.GetAllComplaintsAsync();
        }

        public Task<Complaint> GetComplaintByIdAsync(string idComplaint)
        {
            return _complaintRepository.GetComplaintByIdAsync(idComplaint);
        }

        public Task UpdateComplaintAsync(string idComplaint, Complaint complaint)
        {
            return _complaintRepository.UpdateComplaintAsync(idComplaint, complaint);
        }
    }
}