using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Complaint;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ChamaOSindico.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
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
            var complaints = await _complaintService.GetAllAsync();
            return Ok(complaints);
        }

        [HttpGet(nameof(GetAllComplaintsAsyncByUserId))]
        public async Task<IActionResult> GetAllComplaintsAsyncByUserId()
        {
            var idUser = User.GetUserId();
            var complaints = await _complaintService.GetAllByUserIdAsync(idUser);
            return Ok(complaints);
        }

        [HttpGet(nameof(GetComplaintById) + "/{id}")]
        public async Task<IActionResult> GetComplaintById(string id)
        {
            var complaint = await _complaintService.GetByIdAsync(id);

            if (complaint == null) return NotFound("Denúncia não encontrada");

            return Ok(complaint);
        }

        [HttpPost(nameof(CreateComplaint))]
        public async Task<IActionResult> CreateComplaint([FromBody] Complaint complaint)
        {
            complaint.CreatedByUserId = User.GetUserId();
            await _complaintService.CreateAsync(complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Complaint created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Complaint complaint)
        {
            var idComplaint = complaint.Id;
            
            var existingComplaint = await _complaintService.GetByIdAsync(complaint.Id);
            if (existingComplaint == null) return NotFound();

            await _complaintService.UpdateAsync(idComplaint, complaint);
            return Ok(ApiResponse<string>.SuccessResult(null, "Denúncia atualizada com sucesso"));
        }

        [HttpPost(nameof(DeleteComplaint))]
        public async Task<IActionResult> DeleteComplaint([FromBody] string id)
        {
            var complaint = await _complaintService.GetByIdAsync(id);

            if (complaint == null) return NotFound();

            await _complaintService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResult(null, "Denúncia removida com sucesso"));
        }

        [HttpPost(nameof(AddAnswerToComplaint))]
        public async Task<IActionResult> AddAnswerToComplaint([FromBody] ComplaintAnswer answer)
        {
            await _complaintService.AddAnswerAsync(answer);
            return Ok(ApiResponse<string>.SuccessResult(null, "Resposta adicionada com sucesso"));
        }

        [HttpPost("ChangeComplaintStatus")]
        public async Task<IActionResult> ChangeComplaintStatus([FromBody] ChangeComplaintStatusDto dto)
        {
            var result = await _complaintService.ChangeComplaintStatusAsync(dto.ComplaintId, dto.Status);
            return Ok(result);
        }
    }

}