using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Complaint
{
    public record ChangeComplaintStatusDto
    {
        public string ComplaintId { get; set; } = string.Empty;
        public ComplaintStatusEnum Status { get; set; }

    }
}
