namespace ChamaOSindico.Application.DTOs.Auth
{
    public record LoginUserDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
