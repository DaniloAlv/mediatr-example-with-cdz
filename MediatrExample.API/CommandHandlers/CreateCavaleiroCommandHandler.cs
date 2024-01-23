using MediatR;
using MediatrExample.API.Commands;
using MediatrExample.API.Domain;
using MediatrExample.API.Notifications;
using MediatrExample.API.Repositories;
using MediatrExample.API.ViewModels;

namespace MediatrExample.API.CommandHandlers
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
            cavaleiro.ReferenciaImagem = "images/unknown.png";

            await _cavaleiroRepository.Adicionar(cavaleiro);

            await _mediator.Publish(new CavaleiroCreatedNotification
            {
                Id = cavaleiro.Id, 
                Imagem = request.Imagem, 
                ReferenciaImagem = cavaleiro.ReferenciaImagem
            });

            return cavaleiro.ParaViewModel();
        }
    }
}
