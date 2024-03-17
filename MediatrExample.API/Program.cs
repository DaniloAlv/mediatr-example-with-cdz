using Amazon.DynamoDBv2;
using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MediatrExample.API.Repositories;
using MediatrExample.Domain.Services;
using MediatrExample.Infrastructure.Configurations;
using MediatrExample.Infrastructure.Repositories;
using MediatrExample.Infrastructure.Services;
using MediatrExample.Infrastructure.Workers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICavaleiroRepository, CavaleiroRepository>();
builder.Services.AddScoped<IS3Service, S3Service>();
builder.Services.AddTransient<IEmailService, EmailService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration");
builder.Services.Configure<EmailConfiguration>(emailConfig);

builder.Services.AddHostedService<EnviaEmailWorker>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddScoped<IAmazonS3, AmazonS3Client>();
builder.Services.AddScoped<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
builder.Services.AddScoped<IAmazonSQS, AmazonSQSClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
