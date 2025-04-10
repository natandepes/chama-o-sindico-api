namespace ChamaOSindico.Domain.Entities
{
    public class PollOption : BaseEntity
    {
         public string? Description { get; set; }
        public int? Votes { get; set; }
    }
}
