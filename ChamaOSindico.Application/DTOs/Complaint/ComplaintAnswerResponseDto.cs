using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Complaint
{
    public record ComplaintAnswerResponseDto
    {
        public string? ComplaintId { get; init; }
        public string? Answer { get; init; }
        public string? AnseredByUserName { get; init; }
        public DateTime AnsweredAt { get; init; }
    }
}
