namespace ChamaOSindico.Application.DTOs.Auth
{
    public record AuthResultDto
    {
        public string Token { get; init; }
        public string Name { get; init; }
        public string UserId { get; init; }
    }
}
