namespace ChamaOSindico.Application.DTOs.Auth
{
    public record AuthUserDto
    {
        public string Id { get; init; }
        public string Email { get; init; }
        public string Role { get; init; }
    }
}
