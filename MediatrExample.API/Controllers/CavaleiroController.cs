using MediatR;
using MediatrExample.Application.Responses;
using MediatrExample.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    [Route("api/cavaleiros")]
    public class CavaleiroController : MainController
    {
        private readonly IMediator _mediatr;

        public CavaleiroController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar(CreateCavaleiroCommand cavaleiro)
        {
            try
            {
                // if (!ModelState.IsValid)
                // {
                //     var erros = ModelState.Values.SelectMany(v => v.Errors);
                //     AdicionarErrosModelState(erros);

                //     throw new InvalidOperationException();
                // }

                var result = await _mediatr.Send(cavaleiro);
                return Created(new Uri(""), ResponseResult.Success(result, StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseResult.Failure(new Error(ex.Message, ex.StackTrace), StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCavaleiroCommand command, [FromRoute] Guid id)
        {
            try
            {
                var responseCommand = await _mediatr.Send(command);
                return Ok(ResponseResult.Success(responseCommand, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseResult.Failure(new Error(ex.Message, ex.StackTrace), StatusCodes.Status400BadRequest)); 
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] DeleteCavaleiroCommand command)
        {
            try
            {
                await _mediatr.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseResult.Failure(new Error(ex.Message, ex.StackTrace), StatusCodes.Status400BadRequest));
            }
        }
    }
}
