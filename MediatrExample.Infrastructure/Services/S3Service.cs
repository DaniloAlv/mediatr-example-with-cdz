using Amazon.S3.Model;
using Amazon.S3;
using MediatrExample.API.ViewModels;
using MediatrExample.API.Notifications;

namespace MediatrExample.API.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _amazonS3;

        public S3Service(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public async Task<Stream> DownloadImagem(CavaleiroViewModel cavaleiro, CancellationToken stoppingToken)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                BucketName = "cavaleiros",
                Key = $"images/{cavaleiro!.Id}"
            };

            var imagemCavaleiro = await _amazonS3.GetObjectAsync(getObjectRequest, stoppingToken);
            return imagemCavaleiro.ResponseStream;
        }

        public async Task<PutObjectResponse> UploadImagem(CavaleiroCreatedNotification notification, CancellationToken stoppingToken)
        {
            string[] imagemValor = notification.ReferenciaImagem.Split('/');
            string bucketName = imagemValor.First();
            string key = string.Join("", imagemValor[1], imagemValor[2]);

            using Stream inputStream = notification.Imagem?.OpenReadStream();

            var putObjectRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = key,
                InputStream = inputStream
            };

            var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest, stoppingToken);
            return putObjectResponse;
        }
    }
}
