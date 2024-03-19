using MediatrExample.Domain.Entities;

namespace MediatrExample.API.Repositories
{
    public interface ICavaleiroRepository
    {
        Task Adicionar(Cavaleiro cavaleiro, CancellationToken cancellationToken);
        Task Atualizar(Cavaleiro cavaleiro, CancellationToken cancellationToken);
        Task<Cavaleiro> ObterPorId(Guid id, CancellationToken cancellationToken);
        Task Remover(Guid id, CancellationToken cancellationToken);
    }
}
