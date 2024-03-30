namespace MediatrExample.Domain.Services;

public interface ISnsService
{
    Task PublicarMensagem<T>(T message, string topicName, CancellationToken cancellationToken = default);
}