using ChamaOSindico.Application.Commands.CreateCondominalService;

namespace ChamaOSindico.WebAPI.Payloads
{
    public sealed record CreateCondominalServicePayload
    {
        public required string Title { get; init; }
        public required string ProviderPhotoUrl { get; init; }
        public required string ProviderName { get; init; }
        public required string Cellphone { get; init; }
        public required string? Description { get; init; }

        public CreateCondominalServiceCommand AsCommand()
            => new(
                Title: Title,
                ProviderPhotoUrl: ProviderPhotoUrl,
                ProviderName: ProviderName,
                Cellphone: Cellphone,
                Description: Description
            );
    }
}