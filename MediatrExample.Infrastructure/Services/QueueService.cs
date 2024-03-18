using Amazon.SQS;
using Amazon.SQS.Model;

public class QueueService : IQueueService
{
    private readonly IAmazonSQS _amazonSQS;

    public QueueService(IAmazonSQS amazonSQS)
    {
        _amazonSQS = amazonSQS;
    }

    public async Task<List<Message>> GetMessages(string queueUrl, CancellationToken cancellationToken = default)
    {
        var receiveMessageResponse = await _amazonSQS.ReceiveMessageAsync(queueUrl, cancellationToken);
        return receiveMessageResponse.Messages;
    }

    public async Task<string> GetQueueUrl(string queueName)
    {
        var queueUrlResponse = await _amazonSQS.GetQueueUrlAsync(queueName);
        return queueUrlResponse.QueueUrl;
    }

    public async Task DeleteMessage(string queueName, string receiptHandle, CancellationToken cancellationToken = default)
    {
        await _amazonSQS.DeleteMessageAsync(queueName, receiptHandle, cancellationToken);
    }
}