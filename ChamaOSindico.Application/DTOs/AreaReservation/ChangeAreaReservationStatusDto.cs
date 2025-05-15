using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.AreaReservation
{
    public record ChangeAreaReservationStatusDto
    {
        public string AreaReservationId { get; set; } = string.Empty;
        public AreaReservationStatusEnum Status { get; set; }
    }
}
