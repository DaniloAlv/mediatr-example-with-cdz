using MediatR;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.Commands
{
    public class UpdateCavaleiroCommand : IRequest<CavaleiroViewModel>
    {
        public string Nome { get; set; }
        public string LocalDeTreinamento { get; set; }
        public string Armadura { get; set; }
        public string Constelacao { get; set; }
        public string GolpePrincipal { get; set; }    
    }
}
