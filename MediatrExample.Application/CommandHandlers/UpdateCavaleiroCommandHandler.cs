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
        var cavaleiroMapeado = request.ParaCavaleiro();
        await _cavaleiroRepository.Atualizar(cavaleiroMapeado, cancellationToken);

        return cavaleiroMapeado.ParaViewModel();
    }
}