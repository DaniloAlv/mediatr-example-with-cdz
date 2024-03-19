using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Mapper;
using MediatrExample.Domain.Events;
using MediatrExample.Domain.Services;
using MediatrExample.Domain.ViewModels;
using Newtonsoft.Json;

namespace MediatrExample.Application.NotificationHandlers
{
    public class CavaleiroCreatedNotificationHandler : INotificationHandler<CavaleiroCreatedNotification>
    {
        private readonly IS3Service _s3Service;
        private readonly ICavaleiroRepository _cavaleiroRepository;
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

        public CavaleiroCreatedNotificationHandler(IS3Service s3Service,
                                                   ICavaleiroRepository cavaleiroRepository,
                                                   IAmazonSimpleNotificationService amazonSimpleNotificationService)
        {
            _s3Service = s3Service;
            _cavaleiroRepository = cavaleiroRepository;
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
        }

        public async Task Handle(CavaleiroCreatedNotification notification, CancellationToken cancellationToken)
        {
            notification.ReferenciaImagem = notification.ReferenciaImagem.Replace("default",
                notification.Id.ToString());

            bool putObjectResponse = await _s3Service.UploadImagem(notification, cancellationToken);

            if (putObjectResponse)
            {
                var cavaleiroExistente = await _cavaleiroRepository.ObterPorId(notification.Id);

                if (cavaleiroExistente is null)
                    throw new NotFoundException("Nenhum cavaleiro foi encontrado.");

                await _cavaleiroRepository.Atualizar(cavaleiroExistente);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Upload de imagem para o cavaleiro {notification.Id} efetuado com sucesso!");

                await PublicarCavaleiroNoSns(cavaleiroExistente.ParaViewModel());
            }
                        
            await Task.CompletedTask;
        }               

        private async Task PublicarCavaleiroNoSns(CavaleiroViewModel cavaleiro)
        {
            var topicArn = await _amazonSimpleNotificationService.FindTopicAsync("cavaleiros");

            string messageToPublish = JsonConvert.SerializeObject(cavaleiro);

            var publishRequest = new PublishRequest()
            {
                TopicArn = topicArn.TopicArn, 
                Message = messageToPublish, 
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String", 
                            StringValue = typeof(CavaleiroViewModel).Name
                        }
                    }
                }
            };

            await _amazonSimpleNotificationService.PublishAsync(publishRequest);            
        }
    }
}
