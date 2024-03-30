using MediatrExample.Domain.Entities;

namespace MediatrExample.API.Repositories
{
    public interface ICavaleiroRepository
    {
        Task Adicionar(Cavaleiro cavaleiro, CancellationToken cancellationToken = default);
        Task Atualizar(Cavaleiro cavaleiro, CancellationToken cancellationToken = default);
        Task<Cavaleiro> ObterPorId(Guid id, CancellationToken cancellationToken = default);
        Task Remover(Guid id, CancellationToken cancellationToken = default);
    }
}
