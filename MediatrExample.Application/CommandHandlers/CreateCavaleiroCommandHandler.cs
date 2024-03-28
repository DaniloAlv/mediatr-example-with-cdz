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
            cavaleiro.ReferenciaImagem = $"{cavaleiro.Divindade}/images/default.png";

            await _cavaleiroRepository.Adicionar(cavaleiro, cancellationToken);

            string fileExtension = request.Imagem?.FileName.Split('.').Last();
            cavaleiro.ReferenciaImagem = $"{cavaleiro.Divindade}/images/{cavaleiro.Id}.{fileExtension}";

            await _mediator.Publish(new CavaleiroCreatedNotification
            {
                Id = Guid.Parse(cavaleiro.Id), 
                Imagem = request.Imagem, 
                ReferenciaImagem = cavaleiro.ReferenciaImagem
            });

            return cavaleiro.ParaViewModel();
        }
    }
}
