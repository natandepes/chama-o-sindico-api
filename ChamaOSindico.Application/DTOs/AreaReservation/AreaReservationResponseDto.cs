using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.AreaReservation
{
    public record AreaReservationResponseDto
    {
        public string? Id { get; init; }
        public string AreaName { get; init; }
        public string CreatedByUserName { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
