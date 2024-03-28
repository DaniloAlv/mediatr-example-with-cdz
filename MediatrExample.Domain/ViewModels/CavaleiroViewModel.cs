namespace MediatrExample.Domain.ViewModels
{
    public class CavaleiroViewModel
    {
        public CavaleiroViewModel() { }
        
        protected CavaleiroViewModel(CavaleiroViewModel model)
        {
            Id = model.Id;
            Nome = model.Nome;
            LocalDeTreinamento = model.LocalDeTreinamento;
            Armadura = model.Armadura;
            Constelacao = model.Constelacao;
            GolpePrincipal = model.GolpePrincipal;
            ReferenciaImagem = model.ReferenciaImagem;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string LocalDeTreinamento { get; set; }
        public string Armadura { get; set; }
        public string Constelacao { get; set; }
        public string GolpePrincipal { get; set; }
        public string ReferenciaImagem { get; set; }
    }
}
