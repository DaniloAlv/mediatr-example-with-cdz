using Amazon.SimpleNotificationService.Model;
using MediatR;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Commands;
using MediatrExample.Application.Mapper;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.CommandHandlers;
public class UpdateCavaleiroCommandHandler : IRequestHandler<UpdateCavaleiroCommand, CavaleiroViewModel>
{
    private readonly ICavaleiroRepository _cavaleiroRepository;

    public UpdateCavaleiroCommandHandler(ICavaleiroRepository cavaleiroRepository)
    {
        _cavaleiroRepository = cavaleiroRepository;
    }

    public async Task<CavaleiroViewModel> Handle(UpdateCavaleiroCommand request, CancellationToken cancellationToken)
    {
        var cavaleiroExistente = await _cavaleiroRepository.ObterPorId(request.Id, cancellationToken);

        if (cavaleiroExistente is null)
            throw new NotFoundException("Cavaleiro não encontrado!");
        
        var cavaleiroMapeado = request.ParaCavaleiro();
        cavaleiroMapeado.ReferenciaImagem = cavaleiroExistente.ReferenciaImagem;     

        await _cavaleiroRepository.Atualizar(cavaleiroMapeado, cancellationToken);

        return cavaleiroMapeado.ParaViewModel();
    }
}