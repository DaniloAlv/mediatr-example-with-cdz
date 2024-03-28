using MediatR;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Commands;

namespace MediatrExample.Application.CommandHandlers
{
    public class DeleteCavaleiroCommandHandler : IRequestHandler<DeleteCavaleiroCommand>
    {
        private readonly ICavaleiroRepository _cavaleiroRepository;

        public DeleteCavaleiroCommandHandler(ICavaleiroRepository cavaleiroRepository)
        {
            _cavaleiroRepository = cavaleiroRepository;
        }

        public async Task Handle(DeleteCavaleiroCommand request, CancellationToken cancellationToken)
        {
            await _cavaleiroRepository.Remover(request.Id, cancellationToken);
            await Task.CompletedTask;
        }
    }
}