using MediatrExample.Domain.Events;
using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Domain.Services
{
    public interface IS3Service
    {
        Task<Stream> DownloadImagem(CavaleiroViewModel cavaleiro, CancellationToken stoppingToken);
        Task<bool> UploadImagem(CavaleiroCreatedNotification notification, CancellationToken stoppingToken);
    }
}
