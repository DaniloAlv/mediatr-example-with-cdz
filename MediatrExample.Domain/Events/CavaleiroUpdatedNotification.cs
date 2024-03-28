using MediatR;
using Microsoft.AspNetCore.Http;

namespace MediatrExample.Domain.Events;

public class CavaleiroUpdatedNotification : INotification
{
    public Guid Id { get; set; }
    public IFormFile? Imagem { get; set; }
    public string ReferenciaImagem { get; set; }
}