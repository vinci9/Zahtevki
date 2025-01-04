using MediatR;
using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;

namespace Zahtevki.Application.Commands
{
    public record UpdateTasksCommand(Guid Id, TasksEntity Tasks) : IRequest<TasksEntity>;

    public class UpdateTasksCommandHandler(ITasksRepository tasksRepository) : IRequestHandler<UpdateTasksCommand, TasksEntity>
    {
        public async Task<TasksEntity> Handle(UpdateTasksCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.UpdateTaskAsync(request.Id, request.Tasks);
        }
    }
}