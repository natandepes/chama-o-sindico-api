using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.AreaReservation
{
    public class AreaReservationDTO
    {
        public string? Id { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
