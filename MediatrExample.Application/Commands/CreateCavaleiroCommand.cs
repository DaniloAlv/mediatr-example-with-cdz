using MediatR;
using MediatrExample.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public string Constelacao { get; set; }

        [Required]
        public string GolpePrincipal { get; set; }

        public IFormFile? Imagem { get; set; }        
    }
}
