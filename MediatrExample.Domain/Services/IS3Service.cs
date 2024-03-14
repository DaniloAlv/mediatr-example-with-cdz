using Amazon.S3.Model;
using MediatrExample.API.Notifications;
using MediatrExample.API.ViewModels;

namespace MediatrExample.API.Services
{
    public interface IS3Service
    {
        Task<Stream> DownloadImagem(CavaleiroViewModel cavaleiro, CancellationToken stoppingToken);
        Task<PutObjectResponse> UploadImagem(CavaleiroCreatedNotification notification, CancellationToken stoppingToken);
    }
}
