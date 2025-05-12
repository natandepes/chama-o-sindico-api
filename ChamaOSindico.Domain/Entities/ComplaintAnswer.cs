namespace ChamaOSindico.Domain.Entities
{
    public class ComplaintAnswer
    {
        public string? ComplaintId { get; set; }
        public string? Answer { get; set; }
        public string? AnsweredByUserId { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
