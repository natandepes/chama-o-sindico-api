using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {   
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet("GetAllComplaints")]
        
        public async Task<IActionResult> GetAllComplaints()
        {
            var listComplaints = await _complaintService.GetAllComplaintsAsync();
            return Ok(listComplaints);
        }

        [HttpGet("GetComplaintById/{id}")]
        public async Task<IActionResult> GetComplaintById(string idComplaint)
        {
            var complaint = await _complaintService.GetComplaintByIdAsync(idComplaint);
            return Ok(complaint);
        }

        [HttpPost("CreateComplaint")]
        public async Task<IActionResult> CreateComplaint(Complaint complaint)
        {
            await _complaintService.CreateComplaintAsync(complaint);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string idComplaint, Complaint complaint)
        {
            await _complaintService.UpdateComplaintAsync(idComplaint, complaint);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(string idComplaint)
        {
            await _complaintService.DeleteComplaintAsync(idComplaint);
            return Ok();
        }
    }
}
