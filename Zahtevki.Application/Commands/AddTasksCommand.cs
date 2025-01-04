using MediatR;
using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;

namespace Zahtevki.Application.Commands
{
    public record AddTasksCommand(TasksEntity Tasks) : IRequest<TasksEntity>;

    public class AddTasksCommandHandler(ITasksRepository tasksRepository) : IRequestHandler<AddTasksCommand, TasksEntity>
    {
        public async Task<TasksEntity> Handle(AddTasksCommand request, CancellationToken cancellationToken)
        {
            return await tasksRepository.AddTaskAsync(request.Tasks);
        }
    }
}
