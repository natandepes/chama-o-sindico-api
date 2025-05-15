namespace ChamaOSindico.Application.DTOs.AreaReservation
{
    public record AreaReservationAnswerResponseDto
    {
        public string? AreaReservationId { get; init; }
        public string? Answer { get; init; }
        public string? AnsweredByUserName { get; init; }
        public DateTime AnsweredAt { get; init; }
    }
}
