namespace MediatrExample.Domain.ViewModels
{
    public class DetalhesCavaleiroParaEmail : CavaleiroViewModel
    {
        public DetalhesCavaleiroParaEmail(CavaleiroViewModel cavaleiro, byte[] imagem) : base(cavaleiro)
        {
            ImagemAsBase64 = ConvertBytesToBase64String(imagem);
        }

        public string ImagemAsBase64 { get; }

        private string ConvertBytesToBase64String(byte[] imagem)
        {
            string imageExtension = ReferenciaImagem.Split('.').Last();
            return $"data:image/{imageExtension};base64,{Convert.ToBase64String(imagem)}";
        }
    }
}
