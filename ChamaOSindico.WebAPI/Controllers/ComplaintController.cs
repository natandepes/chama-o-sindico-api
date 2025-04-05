using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        // controller de teste de integração com o mongo
        
        private readonly ComplaintRepository _complaintRepository;

        public ComplaintController(ComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        [HttpGet("GetAllComplaints")]
        
        public async Task<IActionResult> GetAllComplaints()
        {
            var listComplaints = await _complaintRepository.GetAllComplaints();
            return Ok(listComplaints);
        }

        [HttpGet("GetComplaintById/{id}")]
        public async Task<IActionResult> GetComplaintById(string idComplaint)
        {
            var complaint = await _complaintRepository.GetComplaintById(idComplaint);
            return Ok(complaint);
        }

        [HttpPost("CreateComplaint")]
        public async Task<IActionResult> CreateComplaint(Complaint complaint)
        {
            await _complaintRepository.CreateComplaint(complaint);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string idComplaint, Complaint complaint)
        {
            await _complaintRepository.UpdateComplaitAsync(idComplaint, complaint);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(string idComplaint)
        {
            await _complaintRepository.DeleteComplaintAsync(idComplaint);
            return Ok();
        }
    }
}
