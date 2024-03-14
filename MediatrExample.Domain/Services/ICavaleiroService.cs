using MediatrExample.API.Commands;
using MediatrExample.API.ViewModels;

namespace MediatrExample.API.Services
{
    public interface ICavaleiroService
    {
        Task<CavaleiroViewModel> Cadastrar(CreateCavaleiroCommand cavaleiro);
        Task ObterTodos();
        Task ObterPorId(Guid Id);
        Task ObterTodosPorArmadura(string armadura);
        Task Atualizar();
    }
}
