namespace ChamaOSindico.Domain.Entities
{
    public class Resident : Person
    {
        public int ApartmentNumber { get; set; }
        public List<Complaint>? Complaints { get; set; } = new List<Complaint>();
    }
}
