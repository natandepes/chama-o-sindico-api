using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Auth
{
    public record RegisterUserDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public UserRoleEnum Role { get; init; }
        public string PersonId { get; init; }
    }
}
