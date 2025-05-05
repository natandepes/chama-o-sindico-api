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
    //[Authorize]
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
            var complaints = await _complaintRepository.GetAllComplaintsAsync();
            return Ok(complaints);
        }
        [HttpGet(nameof(GetAllComplaintsAsyncByUserId))]
        public async Task<IActionResult> GetAllComplaintsAsyncByUserId()
        {
            var idUser = User.GetUserId();
            var complaints = await _complaintRepository.GetAllComplaintsAsyncByUserId(idUser);
            return Ok(complaints);
        }

        [HttpPost(nameof(GetComplaintById))]
        public async Task<IActionResult> GetComplaintById([FromBody] string id)
        {
            var idUser = User.GetUserId();
            var complaint = await _complaintRepository.GetComplaintByIdAsync(id);

            if (complaint == null) return NotFound("Denúncia não encontrada");
            if (complaint.CreatedByUserId != idUser) return Forbid();

            return Ok(complaint);
        }

        [HttpPost(nameof(CreateComplaint))]
        public async Task<IActionResult> CreateComplaint([FromBody] Complaint complaint)
        {
            complaint.CreatedByUserId = User.GetUserId();
            await _complaintRepository.CreateComplaintAsync(complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint created successfully"));
        }

        [HttpPut(nameof(EditComplaint))]
        public async Task<IActionResult> EditComplaint([FromBody] Complaint complaint)
        {
            var userId = User.GetUserId();
            var idComplaint = complaint.Id;
            var existingComplaint = await _complaintRepository.GetComplaintByIdAsync(complaint.Id);
            if (existingComplaint == null) return NotFound();
            if (existingComplaint.CreatedByUserId != userId) return Forbid();

            await _complaintRepository.UpdateComplaintAsync(idComplaint, complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Denúncia atualizada com sucesso"));
        }

        [HttpPost(nameof(DeleteComplaint))]
        public async Task<IActionResult> DeleteComplaint([FromBody] string id)
        {
            var userId = User.GetUserId();
            var complaint = await _complaintRepository.GetComplaintByIdAsync(id);

            if (complaint == null) return NotFound();
            if (complaint.CreatedByUserId != userId) return Forbid();

            await _complaintRepository.DeleteComplaintAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Denúncia removida com sucesso"));
        }
    }

}