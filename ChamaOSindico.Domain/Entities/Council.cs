namespace ChamaOSindico.Domain.Entities
{
    public class Council : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Resolution { get; set; }
        public string? Protocol { get; set; }
    }
}
