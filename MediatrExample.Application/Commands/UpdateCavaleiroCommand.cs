using MediatR;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.Commands
{
    public class UpdateCavaleiroCommand : IRequest<CavaleiroViewModel>
    {
        public UpdateCavaleiroCommand(Guid id, UpdateCavaleiroCommand command)
        {
            Id = id;
            Nome = command.Nome;
            LocalDeTreinamento = command.LocalDeTreinamento;
            Armadura = command.Armadura;
            Constelacao = command.Constelacao;
            GolpePrincipal = command.GolpePrincipal;
        }

        public Guid Id { get; }
        public string Nome { get; }
        public string LocalDeTreinamento { get; }
        public string Armadura { get; }
        public string Constelacao { get; }
        public string GolpePrincipal { get; }
    }
}
