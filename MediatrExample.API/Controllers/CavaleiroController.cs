using MediatrExample.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    [Route("api/cavaleiros")]
    public class CavaleiroController : MainController
    {
        private readonly ICavaleiroService _cavaleiroService;

        public CavaleiroController(ICavaleiroService cavaleiroService)
        {
            _cavaleiroService = cavaleiroService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar(CreateCavaleiroCommand cavaleiro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erros = ModelState.Values.SelectMany(v => v.Errors);
                    AdicionarErrosModelState(erros);

                    throw new InvalidOperationException();
                }

                var result = await _cavaleiroService.Cadastrar(cavaleiro);
                return Created(new Uri(""), new ResponseResult(result, StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
