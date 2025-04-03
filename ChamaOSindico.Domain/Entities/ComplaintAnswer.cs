namespace ChamaOSindico.Domain.Entities
{
    public class ComplaintAnswer
    {
        public string? Id { get; set; }
        public string? ComplaintId { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
