using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Complaint
{
    public record ComplaintResponseDto
    {
        public string ComplaintId { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public ComplaintStatusEnum Status { get; init; }
        public string? CreatedByUserName { get; init; }
    }
}
