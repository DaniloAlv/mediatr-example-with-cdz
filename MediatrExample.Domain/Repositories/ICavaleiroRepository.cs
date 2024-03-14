using MediatrExample.API.Domain;

namespace MediatrExample.API.Repositories
{
    public interface ICavaleiroRepository
    {
        Task Adicionar(Cavaleiro cavaleiro);
        Task Atualizar(Cavaleiro cavaleiro);
        Task<Cavaleiro> ObterPorId(Guid id);
        Task Remover(Cavaleiro cavaleiro);
    }
}
