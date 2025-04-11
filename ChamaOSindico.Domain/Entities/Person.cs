namespace ChamaOSindico.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? UserId {  get; set; }
    }
}
