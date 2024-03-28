using MediatR;
using MediatrExample.Domain.Events;

namespace MediatrExample.Application.NotificationHandlers;

public class CavaleiroUpdatedNotificationHandler : INotificationHandler<CavaleiroUpdatedNotification>
{
    public async Task Handle(CavaleiroUpdatedNotification notification, CancellationToken cancellationToken)
    {
        
    }
}
