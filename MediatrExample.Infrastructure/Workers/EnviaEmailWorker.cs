using MediatrExample.Domain.Services;
using MediatrExample.Domain.ViewModels;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MediatrExample.Infrastructure.Workers
{
    public class EnviaEmailWorker : BackgroundService
    {
        private const string queueName = "cavaleiros-criados";
        private readonly IEmailService _emailService;
        private readonly IQueueService _queueService;
        private readonly IS3Service _s3Service;

        public EnviaEmailWorker(IEmailService emailService,
                                IQueueService queueService,
                                IS3Service s3Service)
        {
            _emailService = emailService;
            _queueService = queueService;
            _s3Service = s3Service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    string queueUrl = await _queueService.GetQueueUrl(queueName);
                    var receivedMessages = await _queueService.GetMessages(queueUrl, stoppingToken);

                    foreach (var message in receivedMessages)
                    {
                        var cavaleiro = JsonConvert.DeserializeObject<CavaleiroViewModel>(message.Body);
                        Stream streamImagem = await _s3Service.DownloadImagem(cavaleiro!, stoppingToken);

                        var imagemComoBytes = ConverteStreamParaArray(streamImagem);

                        var detalhesParaEmail = new DetalhesCavaleiroParaEmail(cavaleiro!, imagemComoBytes);
                        await _emailService.EnviarEmail(detalhesParaEmail);

                        await _queueService.DeleteMessage(queueUrl, message.ReceiptHandle, stoppingToken);
                    }                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    await Task.Delay(15000);
                }
            }
        }

        private byte[] ConverteStreamParaArray(Stream streamImagem)
        {
            using var memoryStream = new MemoryStream();
            streamImagem.CopyTo(memoryStream);
            var imagemComoBytes = memoryStream.ToArray();

            return imagemComoBytes;
        }

        // private T CreateServiceScope<T>() where T : notnull
        // {
        //     using IServiceScope scope = _serviceScopeFactory.CreateScope();
        //     return scope.ServiceProvider.GetRequiredService<T>();
        // }
    }
}
