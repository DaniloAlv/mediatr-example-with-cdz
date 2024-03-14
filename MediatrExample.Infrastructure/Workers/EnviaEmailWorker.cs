using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using MediatrExample.API.Services;
using MediatrExample.API.ViewModels;
using Newtonsoft.Json;

namespace MediatrExample.Infrastructure.Workers
{
    public class EnviaEmailWorker : BackgroundService
    {
        private const string queueName = "cavaleiros-criados";
        private readonly IAmazonSQS _amazonSQS;
        private readonly IS3Service _s3Service;
        private readonly IEmailService _emailService;

        public EnviaEmailWorker(IAmazonSQS amazonSQS,
                                IEmailService emailService,
                                IS3Service s3Service)
        {
            _amazonSQS = amazonSQS;
            _emailService = emailService;
            _s3Service = s3Service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueResponse = await _amazonSQS.GetQueueUrlAsync(queueName);
            var receiveMessageResponse = await _amazonSQS.ReceiveMessageAsync(queueResponse.QueueUrl, stoppingToken);

            foreach(var message in receiveMessageResponse.Messages)
            {
                var cavaleiro = JsonConvert.DeserializeObject<CavaleiroViewModel>(message.Body);
                Stream streamImagem = await _s3Service.DownloadImagem(cavaleiro!, stoppingToken);

                byte[] imagemComoBytes = ConverteStreamParaArray(streamImagem);

                var detalhesParaEmail = new DetalhesCavaleiroParaEmail(cavaleiro!, imagemComoBytes);
                await _emailService.EnviarEmail(detalhesParaEmail);

                await _amazonSQS.DeleteMessageAsync(queueResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
            }
        }

        private byte[] ConverteStreamParaArray(Stream streamImagem)
        {
            using var memoryStream = new MemoryStream();
            streamImagem.CopyTo(memoryStream);
            var imagemComoBytes = memoryStream.ToArray();

            return imagemComoBytes;
        }
    }
}
