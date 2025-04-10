namespace ChamaOSindico.Domain.Entities
{
    public class ComplaintAnswer : BaseEntity
    {
     
        public string? ComplaintId { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
