namespace ChamaOSindico.Domain.Entities
{
    public class Apartment : BaseEntity
    {
        public int ApartmentNumber { get; set; }
        public List<Resident>? Residents { get; set; } = new List<Resident>();
        public List<Visitor>? Visitors { get; set; } = new List<Visitor>();
        public List<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
        public List<AreaReservation>? AreaReservations { get; set; } = new List<AreaReservation>();
    }
}
