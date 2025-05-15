using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs.Complaint;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using ChamaOSindico.Domain.Interfaces;
using System.Net;

namespace ChamaOSindico.Application.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly ICondominalManagerRepository _condominalManagerRepository;
        private readonly IWarningService _warningService;

        public ComplaintService(IComplaintRepository complaintRepository, IResidentRepository residentRepository, ICondominalManagerRepository condominalManagerRepository, IWarningService warningService)
        {
            _complaintRepository = complaintRepository;
            _residentRepository = residentRepository;
            _condominalManagerRepository = condominalManagerRepository;
            _warningService = warningService;
        }

        public async Task<ApiResponse<List<ComplaintResponseDto>>> GetAllAsync()
        {
            var list = await _complaintRepository.GetAllComplaintsAsync();

            var userIds = list
               .Where(v => !string.IsNullOrEmpty(v.CreatedByUserId))
               .Select(v => v.CreatedByUserId)
               .Distinct()
               .ToList();

            var residents = await _residentRepository
                .GetResidentsByUserIdAsync(userIds);

            var userIdNameMap = residents.ToDictionary(r => r.UserId, r => r.Name);


            var complaintResponseDtos = list.Select(c => new ComplaintResponseDto
            {
                ComplaintId = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                CreatedByUserName = userIdNameMap.TryGetValue(c.CreatedByUserId, out var name) ? name : "",
                Status = c.Status
            }).ToList();

            return ApiResponse<List<ComplaintResponseDto>>.SuccessResult(complaintResponseDtos, "Denúncias carregadas com sucesso.");
        }

        public async Task<ApiResponse<List<ComplaintResponseDto>>> GetAllByUserIdAsync(string userId)
        {
            var list = await _complaintRepository.GetAllComplaintsAsyncByUserId(userId);
            
            var userIds = list
                .Where(v => !string.IsNullOrEmpty(v.CreatedByUserId))
                .Select(v => v.CreatedByUserId)
                .Distinct()
                .ToList();

            var residents = await _residentRepository
                .GetResidentsByUserIdAsync(userIds);

            var userIdNameMap = residents.ToDictionary(r => r.UserId, r => r.Name);


            var complaintResponseDtos = list.Select(c => new ComplaintResponseDto
            {
                ComplaintId = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                CreatedByUserName = userIdNameMap.TryGetValue(c.CreatedByUserId, out var name) ? name : null,
                Status = c.Status
            }).ToList();

            return ApiResponse<List<ComplaintResponseDto>>.SuccessResult(complaintResponseDtos, "Denúncias do usuário carregadas.");
        }

        public async Task<ApiResponse<ComplaintFullResponseDto>> GetByIdAsync(string id)
        {
            var complaint = await _complaintRepository.GetComplaintByIdAsync(id);

            if (complaint == null)
                return ApiResponse<ComplaintFullResponseDto>.ErrorResult("Denúncia não encontrada.", HttpStatusCode.NotFound);

            var createdByResident = await _residentRepository.GetResidentByUserIdAsync(complaint.CreatedByUserId!);

            string? closedByName = null;
            if (!string.IsNullOrWhiteSpace(complaint.ClosedByUserId))
            {
                var closedBy = await _condominalManagerRepository.GetCondominalManagerByUserIdAsync(complaint.ClosedByUserId!);
                closedByName = closedBy?.Name;
            }

            var answers = new List<ComplaintAnswerResponseDto>();

            foreach (var answer in complaint.Answers)
            {
                var manager = await _condominalManagerRepository.GetCondominalManagerByUserIdAsync(answer.AnsweredByUserId!);

                answers.Add(new ComplaintAnswerResponseDto
                {
                    ComplaintId = answer.ComplaintId,
                    Answer = answer.Answer,
                    AnseredByUserName = manager?.Name,
                    AnsweredAt = answer.AnsweredAt
                });
            }

            var dto = new ComplaintFullResponseDto
            {
                Title = complaint.Title,
                Description = complaint.Description,
                ImageUrl = complaint.ImageUrl,
                Status = complaint.Status,
                CreatedAt = complaint.CreatedAt,
                ClosedAt = complaint.ClosedAt,
                CreatedByUserName = createdByResident?.Name,
                ClosedByUserName = closedByName,
                Answers = answers
            };

            return ApiResponse<ComplaintFullResponseDto>.SuccessResult(dto, "Denúncia carregada.");
        }

        public async Task<ApiResponse<string>> CreateAsync(Complaint complaint)
        {
            await _complaintRepository.CreateComplaintAsync(complaint);
            return ApiResponse<string>.SuccessResult(null, "Denúncia criada com sucesso.");
        }

        public async Task<ApiResponse<string>> UpdateAsync(string id, Complaint updatedComplaint)
        {
            var existing = await _complaintRepository.GetComplaintByIdAsync(id);

            if (existing == null)
                return ApiResponse<string>.ErrorResult("Denúncia não encontrada.", HttpStatusCode.NotFound);

            updatedComplaint.Id = id; // preserve ID
            await _complaintRepository.UpdateComplaintAsync(id, updatedComplaint);
            return ApiResponse<string>.SuccessResult(null, "Denúncia atualizada.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(string id)
        {
            var existing = await _complaintRepository.GetComplaintByIdAsync(id);
            if (existing == null)
                return ApiResponse<string>.ErrorResult("Denúncia não encontrada.", HttpStatusCode.NotFound);

            await _complaintRepository.DeleteComplaintAsync(id);
            return ApiResponse<string>.SuccessResult(null, "Denúncia excluída.");
        }

        public async Task<ApiResponse<string>> AddAnswerAsync(ComplaintAnswer answer)
        {
            var complaint = await _complaintRepository.GetComplaintByIdAsync(answer.ComplaintId!);
            if (complaint == null)
                return ApiResponse<string>.ErrorResult("Denúncia não encontrada para responder.", HttpStatusCode.NotFound);

            await _complaintRepository.AddAnswerToComplaintAsync(answer);

            var warning = new Warning
            {
                Title = "Nova resposta sobre denúncia",
                Description = $"Sua denúncia de título: {complaint.Title} obteve uma nova resposta do síndico!",
                TargetType = "specific",
                ResidentUserId = complaint.CreatedByUserId
            };

            await _warningService.CreateWarningAsync(warning);

            return ApiResponse<string>.SuccessResult(null, "Resposta registrada com sucesso.");
        }

        public async Task<ApiResponse<string>> ChangeComplaintStatusAsync(string complaintId, ComplaintStatusEnum newStatus)
        {
            var complaint = await _complaintRepository.GetComplaintByIdAsync(complaintId);

            if (complaint == null)
            {
                return ApiResponse<string>.ErrorResult("Denúncia não encontrada.", HttpStatusCode.NotFound);
            }

            complaint.Status = newStatus;

            if (newStatus == ComplaintStatusEnum.Resolved)
            {
                complaint.ClosedAt = DateTime.UtcNow;
            }

            await _complaintRepository.UpdateComplaintAsync(complaintId, complaint);

            var warning = new Warning
            {
                Title = "Atualização de status de denúncia",
                Description = $"Sua denúncia de título: {complaint.Title} teve o seu status atualizado pelo síndico!",
                TargetType = "specific",
                ResidentUserId = complaint.CreatedByUserId
            };

            await _warningService.CreateWarningAsync(warning);

            return ApiResponse<string>.SuccessResult(null, "Status da denúncia atualizado com sucesso.");
        }
    }
}
