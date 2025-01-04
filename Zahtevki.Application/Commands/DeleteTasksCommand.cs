using MediatR;
using Zahtevki.Core.Interfaces;

namespace Zahtevki.Application.Commands
{
    public record DeleteTasksCommand(Guid Id) : IRequest<bool>;

    public class DeleteTasksCommandHandler(ITasksRepository tasksRepository) : IRequestHandler<DeleteTasksCommand, bool>
    {
        public async Task<bool> Handle(DeleteTasksCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.DeleteTaskAsync(request.Id);
        }
    }
}