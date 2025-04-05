namespace ChamaOSindico.Domain.Entities
{
    public class Poll : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<PollOption> Options { get; set; } = new List<PollOption>();
    }
}
