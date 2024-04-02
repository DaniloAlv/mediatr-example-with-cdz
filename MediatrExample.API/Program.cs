using MediatrExample.Infrastructure.Configurations;
using MediatrExample.Infrastructure.Workers;
using System.Reflection;
using MediatrExample.API.Extensions;
using MediatrExample.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration");
builder.Services.Configure<EmailConfiguration>(emailConfig);

builder.Services.AddHostedService<EnviaEmailWorker>();

builder.Services.AddDependencyInjectionsConfiguration();
builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ApplicationAssemblyReference)))
);

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
