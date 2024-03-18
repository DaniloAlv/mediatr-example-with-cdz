using Amazon.SQS.Model;

public interface IQueueService
{
    Task<string> GetQueueUrl(string queueName);
    Task<List<Message>> GetMessages(string queueUrl, CancellationToken cancellationToken = default);
    Task DeleteMessage(string queueName, string receiptHandle, CancellationToken cancellationToken = default);
}