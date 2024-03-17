using Amazon.SQS;
using MediatrExample.Domain.Services;
using MediatrExample.Domain.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MediatrExample.Infrastructure.Workers
{
    public class EnviaEmailWorker : BackgroundService
    {
        private const string queueName = "cavaleiros-criados";
        private readonly IEmailService _emailService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EnviaEmailWorker(IEmailService emailService,
                                IServiceScopeFactory serviceScopeFactory)
        {
            _emailService = emailService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IAmazonSQS amazonSQS = CreateServiceScope<IAmazonSQS>();
            IS3Service s3Service = CreateServiceScope<IS3Service>();
            
            var queueResponse = await amazonSQS.GetQueueUrlAsync(queueName);
            var receiveMessageResponse = await amazonSQS.ReceiveMessageAsync(queueResponse.QueueUrl, stoppingToken);

            foreach(var message in receiveMessageResponse.Messages)
            {
                var cavaleiro = JsonConvert.DeserializeObject<CavaleiroViewModel>(message.Body);
                Stream streamImagem = await s3Service.DownloadImagem(cavaleiro!, stoppingToken);

                byte[] imagemComoBytes = ConverteStreamParaArray(streamImagem);

                var detalhesParaEmail = new DetalhesCavaleiroParaEmail(cavaleiro!, imagemComoBytes);
                await _emailService.EnviarEmail(detalhesParaEmail);

                await amazonSQS.DeleteMessageAsync(queueResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
            }
        }

        private byte[] ConverteStreamParaArray(Stream streamImagem)
        {
            using var memoryStream = new MemoryStream();
            streamImagem.CopyTo(memoryStream);
            var imagemComoBytes = memoryStream.ToArray();

            return imagemComoBytes;
        }

        private T CreateServiceScope<T>() where T : notnull
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}
