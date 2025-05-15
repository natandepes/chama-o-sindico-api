using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Complaint
{
    public record ComplaintFullResponseDto
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public ComplaintStatusEnum Status { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? ClosedAt { get; init; }
        public string? CreatedByUserName { get; init; }
        public string? ClosedByUserName { get; init; }
        public List<ComplaintAnswerResponseDto> Answers { get; init; } = new List<ComplaintAnswerResponseDto>();
    }
}
