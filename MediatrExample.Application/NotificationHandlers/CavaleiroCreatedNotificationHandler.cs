using Amazon.SimpleNotificationService.Model;
using MediatR;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Mapper;
using MediatrExample.Domain.Events;
using MediatrExample.Domain.Services;

namespace MediatrExample.Application.NotificationHandlers
{
    public class CavaleiroCreatedNotificationHandler : INotificationHandler<CavaleiroCreatedNotification>
    {
        private readonly IS3Service _s3Service;
        private readonly ICavaleiroRepository _cavaleiroRepository;
        private readonly ISnsService _snsService;

        public CavaleiroCreatedNotificationHandler(IS3Service s3Service,
                                                   ICavaleiroRepository cavaleiroRepository,
                                                   ISnsService snsService)
        {
            _s3Service = s3Service;
            _cavaleiroRepository = cavaleiroRepository;
            _snsService = snsService;
        }

        public async Task Handle(CavaleiroCreatedNotification notification, CancellationToken cancellationToken)
        {
            bool putObjectResponse = await _s3Service.UploadImagem(notification, cancellationToken);

            if (putObjectResponse)
            {
                var cavaleiroExistente = await _cavaleiroRepository.ObterPorId(notification.Id, cancellationToken);

                if (cavaleiroExistente is null)
                    throw new NotFoundException("Nenhum cavaleiro foi encontrado.");

                cavaleiroExistente.ReferenciaImagem = notification.ReferenciaImagem;
                await _cavaleiroRepository.Atualizar(cavaleiroExistente, cancellationToken);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Upload de imagem para o cavaleiro {notification.Id} efetuado com sucesso!");

                await _snsService.PublicarMensagem(cavaleiroExistente.ParaViewModel(), "cavaleiros", cancellationToken);
            }
            
            await Task.CompletedTask;
        }
    }
}
