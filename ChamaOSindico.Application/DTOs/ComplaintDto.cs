using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs
{

    public class ComplaintDto: BaseDto
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public ComplaintStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public ComplaintDto(Complaint complaint)
        {
            Id = complaint.Id;
            Title = complaint.Title;
            Description = complaint.Description;
            ImageUrl = complaint.ImageUrl;
            Status = complaint.Status;
            CreatedAt = complaint.CreatedAt;
            ClosedAt = complaint.ClosedAt;
        }
    };

    
}