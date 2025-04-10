using MediatR;

namespace ChamaOSindico.Application.Commands.CreateCondominalService
{
    public sealed record CreateCondominalServiceCommand(
        string Title,
        string ProviderPhotoUrl,
        string ProviderName,
        string Cellphone,
        string? Description
    ) : IRequest<Guid>;
}
