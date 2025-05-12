using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class Complaint : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public ComplaintStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? CreatedByUserId { get; set; }
        public string? ClosedByUserId { get; set; }
        public List<ComplaintAnswer> Answers { get; set; } = new List<ComplaintAnswer>();
    }
}
