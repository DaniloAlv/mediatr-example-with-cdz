using MediatrExample.API.ViewModels;

namespace MediatrExample.API.Services
{
    public interface IEmailService
    {
        Task<string> EnviarEmail(DetalhesCavaleiroParaEmail detalhes);
    }
}
