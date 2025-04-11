using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs
{
    public class AreaReservationDTO
    {
        public string? Id { get; set; }
        public string? AreaId { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
