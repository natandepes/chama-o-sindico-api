namespace ChamaOSindico.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
