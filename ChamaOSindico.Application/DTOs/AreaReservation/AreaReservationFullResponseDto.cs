using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.AreaReservation
{
    public record AreaReservationFullResponseDto
    {
        public string AreaName { get; init; }
        public string AreaId { get; init; }
        public string CreatedByUserName { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public AreaReservationStatusEnum Status { get; init; }
        public List<AreaReservationAnswerResponseDto> Answers { get; init; } = new List<AreaReservationAnswerResponseDto>();
    }
}
