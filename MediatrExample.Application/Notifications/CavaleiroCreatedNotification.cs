using MediatR;

namespace MediatrExample.API.Notifications
{
    public class CavaleiroCreatedNotification : INotification
    {
        public Guid Id { get; set; }
        public IFormFile? Imagem { get; set; }
        public string ReferenciaImagem { get; set; }
    }
}
