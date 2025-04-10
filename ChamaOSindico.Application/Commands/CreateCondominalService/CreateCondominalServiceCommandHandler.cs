using ChamaOSindico.Domain.Entities;
using MediatR;

namespace ChamaOSindico.Application.Commands.CreateCondominalService
{
    public class CreateCondominalServiceHandler : IRequestHandler<CreateCondominalServiceCommand, Guid>
    { 
        // To be done
        // public class CreateCondominalServiceHandler(ICondominalServiceRepository repository)

        public Task<Guid> Handle(CreateCondominalServiceCommand request, CancellationToken cancellationToken)
        {
            var condominalService = new CondominalService(
                title: request.Title.Trim(),
                providerPhotoUrl: request.ProviderPhotoUrl.Trim(),
                providerName: request.ProviderName.Trim(),
                cellphone: request.Cellphone.Trim(),
                description: request.Description?.Trim()
            );

            // To be done
            //await _repository.AddAsync(condominalService);
            return Task.FromResult(condominalService.Id); // Once the repository is working this Task.FromResult won't be needed
        }

    }
}
