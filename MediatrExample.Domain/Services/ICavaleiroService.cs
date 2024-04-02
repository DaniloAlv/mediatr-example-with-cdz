using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Domain.Services;

public interface ICavaleiroService
{
    Task<CavaleiroViewModel> ObterPorId(Guid id);
}