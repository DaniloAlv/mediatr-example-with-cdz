using MediatR;
using MediatrExample.Application.Responses;
using MediatrExample.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatrExample.Domain.Services;
using Amazon.SimpleNotificationService.Model;

namespace MediatrExample.API.Controllers
{
    [Route("api/cavaleiros")]
    public class CavaleiroController : MainController
    {
        private readonly IMediator _mediatr;
        private readonly ICavaleiroService _cavaleiroService;

        public CavaleiroController(IMediator mediatr, 
                                   ICavaleiroService cavaleiroService)
        {
            _mediatr = mediatr;
            _cavaleiroService = cavaleiroService;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            try
            {
                var cavaleiro = await _cavaleiroService.ObterPorId(id);
                return Ok(ResponseResult.Success(cavaleiro, StatusCodes.Status200OK));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ResponseResult.Failure(new Error(ex.Message, ex.InnerException?.Message), StatusCodes.Status404NotFound));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar([FromForm] CreateCavaleiroCommand cavaleiro)
        {
            try
            {
                var result = await _mediatr.Send(cavaleiro);
                return Created(new Uri($"https://localhost:7287/api/cavaleiros/{result.Id}"), ResponseResult.Success(result, StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseResult.Failure(new Error(ex.Message, ex.StackTrace), StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdateCavaleiroCommand command, [FromRoute] Guid id)
        {
            try
            {
                var updatedCommand = new UpdateCavaleiroCommand(id, command);
                var responseCommand = await _mediatr.Send(updatedCommand);
                return Ok(ResponseResult.Success(responseCommand, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseResult.Failure(new Error(ex.Message, ex.StackTrace), StatusCodes.Status400BadRequest)); 
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var command = new DeleteCavaleiroCommand(id);
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
