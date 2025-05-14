using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Complaint;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.Interfaces
{
    public interface IComplaintService
    {
        Task<ApiResponse<List<ComplaintResponseDto>>> GetAllAsync();
        Task<ApiResponse<List<ComplaintResponseDto>>> GetAllByUserIdAsync(string userId);
        Task<ApiResponse<ComplaintFullResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<string>> CreateAsync(Complaint complaint);
        Task<ApiResponse<string>> UpdateAsync(string id, Complaint updatedComplaint);
        Task<ApiResponse<string>> DeleteAsync(string id);
        Task<ApiResponse<string>> AddAnswerAsync(ComplaintAnswer answer);
        Task<ApiResponse<string>> ChangeComplaintStatusAsync(string complaintId, ComplaintStatusEnum newStatus);

    }
}
