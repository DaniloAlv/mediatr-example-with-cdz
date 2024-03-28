using MediatR;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.Commands
{
    public class UpdateCavaleiroCommand : IRequest<CavaleiroViewModel>
    {
        public UpdateCavaleiroCommand()
        {
            
        }

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
        public string? Nome { get; set; }
        public string? LocalDeTreinamento { get; set; }
        public string? Armadura { get; set; }
        public string? Constelacao { get; set; }
        public string? GolpePrincipal { get; set; }
    }
}
