namespace MediatrExample.API.ViewModels
{
    public class DetalhesCavaleiroParaEmail : CavaleiroViewModel
    {
        public DetalhesCavaleiroParaEmail(CavaleiroViewModel cavaleiro, byte[] imagem) : base(cavaleiro)
        {
            Imagem = imagem;
        }

        public byte[] Imagem { get; set; }
    }
}
