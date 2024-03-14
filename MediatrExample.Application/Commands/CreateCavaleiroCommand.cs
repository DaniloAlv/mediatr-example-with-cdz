using MediatR;
using MediatrExample.API.Domain;
using MediatrExample.API.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MediatrExample.Application.Commands
{
    public class CreateCavaleiroCommand : IRequest<CavaleiroViewModel>
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string LocalDeTreinamento { get; set; }

        [Required]
        public string Armadura { get; set; }

        [Required]
        public string Constelação { get; set; }

        [Required]
        public string GolpePrincipal { get; set; }

        public IFormFile? Imagem { get; set; }


        public Cavaleiro ParaCavaleiro()
        {
            return new Cavaleiro
            {
                Nome = Nome,
                Armadura = Armadura, 
                Constelação = Constelação, 
                GolpePrincipal = GolpePrincipal, 
                LocalDeTreinamento = LocalDeTreinamento,
            };
        }
    }
}
