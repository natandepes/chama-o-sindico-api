namespace ChamaOSindico.Application.DTOs.User
{
    public record UserResponseDto
    {
        public string Id { get; init; }
        public string Email { get; init; }
        public string Role { get; init; }
        public string PersonId { get; init; }
    }
}
