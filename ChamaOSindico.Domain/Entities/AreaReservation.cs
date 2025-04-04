using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class AreaReservation
    {
        public string? Id { get; set; }
        public string? AreaId { get; set; }
        public string? ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
