using MediatrExample.Application.Commands;
using MediatrExample.Domain.Entities;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Application.Mapper
{
    public static class CavaleiroMapper
    {
        public static CavaleiroViewModel ParaViewModel(this Cavaleiro cavaleiro)
        {
            return new CavaleiroViewModel
            {
                Id = Guid.Parse(cavaleiro.Id), 
                Nome = cavaleiro.Nome,
                Armadura = cavaleiro.Armadura,
                Constelacao = cavaleiro.Constelacao, 
                GolpePrincipal = cavaleiro.GolpePrincipal, 
                LocalDeTreinamento = cavaleiro.LocalDeTreinamento,
                Divindade = cavaleiro.Divindade, 
                ReferenciaImagem = cavaleiro.ReferenciaImagem
            };
        }

        public static Cavaleiro ParaCavaleiro(this CavaleiroViewModel cavaleiroViewModel)
        {
            return new Cavaleiro(cavaleiroViewModel.Id.ToString())
            {
                Nome = cavaleiroViewModel.Nome, 
                Armadura = cavaleiroViewModel.Armadura, 
                Constelacao = cavaleiroViewModel.Constelacao,
                GolpePrincipal = cavaleiroViewModel.GolpePrincipal,
                LocalDeTreinamento = cavaleiroViewModel.LocalDeTreinamento,
                ReferenciaImagem = cavaleiroViewModel.ReferenciaImagem
            };
        }

        public static Cavaleiro ParaCavaleiro(this CreateCavaleiroCommand cavaleiro)
        {
            return new Cavaleiro()
            {
                Nome = cavaleiro.Nome,
                Armadura = cavaleiro.Armadura,                 
                Constelacao = cavaleiro.Constelacao, 
                GolpePrincipal = cavaleiro.GolpePrincipal, 
                LocalDeTreinamento = cavaleiro.LocalDeTreinamento,
            };
        }

        public static Cavaleiro ParaCavaleiro(this UpdateCavaleiroCommand command)
        {
            return new Cavaleiro(command.Id.ToString())
            {
                Nome = command.Nome,
                Armadura = command.Armadura, 
                Constelacao = command.Constelacao, 
                GolpePrincipal = command.GolpePrincipal, 
                LocalDeTreinamento = command.LocalDeTreinamento
            };
        }
    }
}