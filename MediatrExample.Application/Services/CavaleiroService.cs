using MediatR;
using MediatrExample.API.Commands;
using MediatrExample.API.ViewModels;

namespace MediatrExample.Application.Services
{
    public class CavaleiroService : ICavaleiroService
    {
        private readonly IMediator _mediator;

        public CavaleiroService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Atualizar()
        {
            throw new NotImplementedException();
        }

        public async Task<CavaleiroViewModel> Cadastrar(CreateCavaleiroCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        public Task ObterPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task ObterTodosPorArmadura(string armadura)
        {
            throw new NotImplementedException();
        }
    }
}
