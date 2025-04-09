using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class User
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRoleEnum Role { get; set; }
        public string? PersonId { get; set; }
    }
}
