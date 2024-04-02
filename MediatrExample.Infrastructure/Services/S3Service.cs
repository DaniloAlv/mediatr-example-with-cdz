using Amazon.S3.Model;
using Amazon.S3;
using MediatrExample.Domain.ViewModels;
using MediatrExample.Domain.Services;
using MediatrExample.Domain.Events;

namespace MediatrExample.Infrastructure.Services
{
    public class S3Service : IS3Service
    {
        private const string bucketName = "cavaleiros";
        private readonly IAmazonS3 _amazonS3;

        public S3Service(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public async Task<Stream> DownloadImagem(CavaleiroViewModel cavaleiro, CancellationToken stoppingToken)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = cavaleiro.ReferenciaImagem
            };

            var imagemCavaleiro = await _amazonS3.GetObjectAsync(getObjectRequest, stoppingToken);
            return imagemCavaleiro.ResponseStream;
        }

        public async Task<bool> UploadImagem(CavaleiroCreatedNotification notification, CancellationToken stoppingToken)
        {
            using Stream inputStream = notification.Imagem?.OpenReadStream();

            var putObjectRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = notification.ReferenciaImagem,
                InputStream = inputStream
            };

            var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest, stoppingToken);
            return putObjectResponse.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
