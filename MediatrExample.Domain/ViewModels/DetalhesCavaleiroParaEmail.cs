namespace MediatrExample.Domain.ViewModels
{
    public class DetalhesCavaleiroParaEmail : CavaleiroViewModel
    {
        public DetalhesCavaleiroParaEmail(CavaleiroViewModel cavaleiro, byte[] imagem) : base(cavaleiro)
        {
            ImagemAsBase64 = Convert.ToBase64String(imagem);
        }

        public string ImagemAsBase64 { get; }
    }
}
