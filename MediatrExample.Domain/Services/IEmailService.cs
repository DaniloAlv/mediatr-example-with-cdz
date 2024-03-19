using MediatrExample.Domain.ViewModels;

namespace MediatrExample.Domain.Services
{
    public interface IEmailService
    {
        Task<string> EnviarEmail(DetalhesCavaleiroParaEmail detalhes);
    }
}
