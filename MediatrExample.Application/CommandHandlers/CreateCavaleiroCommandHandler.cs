using MediatR;
using MediatrExample.Application.Commands;
using MediatrExample.API.Repositories;
using MediatrExample.Domain.Entities;
using MediatrExample.Domain.ViewModels;
using MediatrExample.Domain.Events;
using MediatrExample.Application.Mapper;

namespace MediatrExample.Application.CommandHandlers
{
    public class CreateCavaleiroCommandHandler : IRequestHandler<CreateCavaleiroCommand, CavaleiroViewModel>
    {
        private readonly IMediator _mediator;
        private readonly ICavaleiroRepository _cavaleiroRepository;

        public CreateCavaleiroCommandHandler(IMediator mediator, 
                                             ICavaleiroRepository cavaleiroRepository)
        {
            _mediator = mediator;
            _cavaleiroRepository = cavaleiroRepository;
        }

        public async Task<CavaleiroViewModel> Handle(CreateCavaleiroCommand request, CancellationToken cancellationToken)
        {
            Cavaleiro cavaleiro = request.ParaCavaleiro();
            cavaleiro.ReferenciaImagem = "atena/images/default.png";

            await _cavaleiroRepository.Adicionar(cavaleiro, cancellationToken);

            string fileExtension = request.Imagem?.FileName.Split('.').Last();

            await _mediator.Publish(new CavaleiroCreatedNotification
            {
                Id = Guid.Parse(cavaleiro.Id), 
                Imagem = request.Imagem, 
                ReferenciaImagem = $"atena/images/{cavaleiro.Id}.{fileExtension}"
            });

            return cavaleiro.ParaViewModel();
        }
    }
}
