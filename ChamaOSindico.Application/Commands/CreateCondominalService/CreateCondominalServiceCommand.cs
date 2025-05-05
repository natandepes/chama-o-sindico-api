using MediatR;

namespace ChamaOSindico.Application.Commands.CreateCondominalService
{
    public sealed record CreateCondominalServiceCommand(
        string Id,
        string Title,
        string ProviderPhotoUrl,
        string ProviderName,
        string Cellphone,
        string? Description
    ) : IRequest<string>;
}
