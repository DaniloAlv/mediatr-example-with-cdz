using Amazon.SimpleNotificationService.Model;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Mapper;
using MediatrExample.Domain.Services;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.Services;

public class CavaleiroService : ICavaleiroService
{
    private readonly ICavaleiroRepository _cavaleiroRepository;

    public CavaleiroService(ICavaleiroRepository cavaleiroRepository)
    {
        _cavaleiroRepository = cavaleiroRepository;
    }

    public async Task<CavaleiroViewModel> ObterPorId(Guid id)
    {
        var cavaleiro = await _cavaleiroRepository.ObterPorId(id);

        if (cavaleiro is null)
            throw new NotFoundException("Cavaleiro não encontrado!");

        return cavaleiro.ParaViewModel();
    }
}
