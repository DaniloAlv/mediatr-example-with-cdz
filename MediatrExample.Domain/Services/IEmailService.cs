using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Domain.Services
{
    public interface IEmailService
    {
        Task EnviarEmail(DetalhesCavaleiroParaEmail detalhes);
    }
}
