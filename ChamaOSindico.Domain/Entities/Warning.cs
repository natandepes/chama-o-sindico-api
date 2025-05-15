namespace ChamaOSindico.Domain.Entities
{
    public class Warning : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TargetType { get; set; }
        public string? ResidentId { get; set; }
        public string? ResidentUserId { get; set; }
    }
}
