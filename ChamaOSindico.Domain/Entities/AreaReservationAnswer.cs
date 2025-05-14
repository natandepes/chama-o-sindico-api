namespace ChamaOSindico.Domain.Entities
{
    public class AreaReservationAnswer
    {
        public string? AreaReservationId { get; set; }
        public string? Answer { get; set; }
        public string? AnsweredByUserId { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
