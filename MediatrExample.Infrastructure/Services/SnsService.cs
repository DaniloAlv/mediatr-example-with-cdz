using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatrExample.Domain.Services;
using Newtonsoft.Json;

namespace MediatrExample.Infrastructure.Services;

public class SnsService : ISnsService
{
    private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

    public SnsService(IAmazonSimpleNotificationService amazonSimpleNotificationService)
    {
        _amazonSimpleNotificationService = amazonSimpleNotificationService;
    }

    public async Task PublicarMensagem<T>(T message, string topicName, CancellationToken cancellationToken)
    {
        var topicArn = await _amazonSimpleNotificationService.FindTopicAsync(topicName);

        string messageToPublish = JsonConvert.SerializeObject(message);

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
                            StringValue = typeof(T).Name
                        }
                    }
                }
        };

        await _amazonSimpleNotificationService.PublishAsync(publishRequest, cancellationToken);
    }
}