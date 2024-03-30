using Amazon.DynamoDBv2;
using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MediatrExample.API.Repositories;
using MediatrExample.Application.Services;
using MediatrExample.Domain.Services;
using MediatrExample.Infrastructure.Repositories;
using MediatrExample.Infrastructure.Services;

namespace MediatrExample.API.Extensions;

public static class DependencyInjectionsConfiguration
{
    public static IServiceCollection AddDependencyInjectionsConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();

        services.AddSingleton<IS3Service, S3Service>();
        services.AddSingleton<IAmazonS3, AmazonS3Client>();

        services.AddScoped<ISnsService, SnsService>();
        services.AddSingleton<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();

        services.AddSingleton<IQueueService, QueueService>();
        services.AddSingleton<IAmazonSQS, AmazonSQSClient>();

        services.AddTransient<IEmailService, EmailService>();
        
        services.AddScoped<ICavaleiroRepository, CavaleiroRepository>();

        services.AddScoped<ICavaleiroService, CavaleiroService>();

        return services;
    }
}