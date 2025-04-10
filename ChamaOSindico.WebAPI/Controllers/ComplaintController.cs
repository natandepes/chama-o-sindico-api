using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComplaintController : ControllerBase
    {   
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet(nameof(GetAllComplaints))]
        public async Task<IActionResult> GetAllComplaints()
        {
            var listComplaints = await _complaintService.GetAllComplaintsAsync();
            return Ok(listComplaints);
        }

        [HttpGet(nameof(GetComplaintById) + "/{id}")]
        public async Task<IActionResult> GetComplaintById(string id)
        {
            var complaint = await _complaintService.GetComplaintByIdAsync(id);
            return Ok(complaint);
        }

        [HttpPost(nameof(CreateComplaint))]
        public async Task<IActionResult> CreateComplaint(Complaint complaint)
        {
            await _complaintService.CreateComplaintAsync(complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint created successfully."));
        }

        [HttpPut(nameof(Update) + "/{id}")]
        public async Task<IActionResult> Update(string id, Complaint complaint)
        {
            await _complaintService.UpdateComplaintAsync(id, complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint updated successfully."));
        }

        [HttpDelete(nameof(DeleteComplaint) + "/{id}")]
        public async Task<IActionResult> DeleteComplaint(string id)
        {
            await _complaintService.DeleteComplaintAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint deleted successfully."));
        }
    }
}
