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
                Id = cavaleiro.Id, 
                Nome = cavaleiro.Nome,
                Armadura = cavaleiro.Armadura,
                Constelacao = cavaleiro.Constelacao, 
                GolpePrincipal = cavaleiro.GolpePrincipal, 
                LocalDeTreinamento = cavaleiro.LocalDeTreinamento
            };
        }

        public static Cavaleiro ParaCavaleiro(this CreateCavaleiroCommand cavaleiro)
        {
            return new Cavaleiro
            {
                Nome = cavaleiro.Nome,
                Armadura = cavaleiro.Armadura,                 
                Constelacao = cavaleiro.Constelação, 
                GolpePrincipal = cavaleiro.GolpePrincipal, 
                LocalDeTreinamento = cavaleiro.LocalDeTreinamento,
            };
        }
    }
}