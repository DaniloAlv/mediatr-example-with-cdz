using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MediatrExample.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private IList<string> _erros = new List<string>();

        public MainController()
        {

        }

        protected void AdicionaMensagemErro(string mensagemErro)
        {
            _erros.Add(mensagemErro);
        }

        protected void AdicionarErrosModelState(IEnumerable<ModelError> erros)
        {
            foreach(ModelError error in erros)
            {
                AdicionaMensagemErro(error.ErrorMessage);
            }
        }

        protected IList<string> ObterErros() 
        {
            return _erros;
        }

        protected bool ExistemErros()
        {
            return _erros.Any();
        }

        protected void LimparLista()
        {
            _erros.Clear();
        }
    }
}
