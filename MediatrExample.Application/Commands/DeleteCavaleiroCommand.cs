using MediatR;

namespace MediatrExample.Application.Commands
{
    public class DeleteCavaleiroCommand : IRequest
    {
        public Guid Id { get; }
    }
}