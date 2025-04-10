using ChamaOSindico.Application.Commom;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComplaintController : ControllerBase
    {   
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintController(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        [HttpGet(nameof(GetAllComplaints))]
        public async Task<IActionResult> GetAllComplaints()
        {
            var listComplaints = await _complaintRepository.GetAllComplaintsAsync();
            return Ok(listComplaints);
        }

        [HttpGet(nameof(GetComplaintById) + "/{id}")]
        public async Task<IActionResult> GetComplaintById(string id)
        {
            var userId = User.GetUserId();

            var complaint = await _complaintRepository.GetComplaintByIdAsync(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found.");
            }

            if (complaint.CreatedByUserId != userId)
            {
                return StatusCode(403, "Access denied. You do not own this complaint");
            }

            return Ok(complaint);
        }

        [HttpPost(nameof(CreateComplaint))]
        public async Task<IActionResult> CreateComplaint(Complaint complaint)
        {
            await _complaintRepository.CreateComplaintAsync(complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint created successfully."));
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public async Task<IActionResult> Update(string id, Complaint complaint)
        {
            await _complaintRepository.UpdateComplaintAsync(id, complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint updated successfully."));
        }

        [HttpDelete(nameof(DeleteComplaint) + "/{id}")]
        public async Task<IActionResult> DeleteComplaint(string id)
        {
            await _complaintRepository.DeleteComplaintAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint deleted successfully."));
        }
    }
}
