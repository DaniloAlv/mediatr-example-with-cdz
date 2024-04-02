using MediatR;

namespace MediatrExample.Application.Commands
{
    public class DeleteCavaleiroCommand : IRequest
    {
        public DeleteCavaleiroCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}